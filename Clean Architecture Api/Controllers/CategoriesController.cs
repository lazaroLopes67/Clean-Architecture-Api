using AutoMapper;
using Criando_Minha_Primeira_API.DTOs;
using Criando_Minha_Primeira_API.Model;
using Criando_Minha_Primeira_API.Pagination;
using Criando_Minha_Primeira_API.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Criando_Minha_Primeira_API.Controllers
{
    /// <summary>
    /// API controller responsible for managing category resources.
    /// 
    /// This controller exposes CRUD endpoints for categories
    /// and uses DTOs to control the data exposed to API clients.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        /// <summary>
        /// Unit of Work instance used to access repositories
        /// and persist changes to the database.
        /// </summary>
        private readonly IUnitOfWork _uof;

        /// <summary>
        /// AutoMapper instance responsible for converting
        /// entities into DTOs and DTOs into entities.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the CategoriesController class.
        /// 
        /// Dependencies are automatically injected by ASP.NET Core's
        /// built-in dependency injection container.
        /// </summary>
        /// <param name="uof">
        /// Unit of Work implementation used to manage repositories
        /// and database transactions.
        /// </param>
        /// <param name="mapper">
        /// AutoMapper instance used for object-object mapping.
        /// </param>
        public CategoriesController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all categories ordered by their Id.
        /// 
        /// The retrieved entities are mapped to CategoryDto objects
        /// before being returned to the client.
        /// </summary>
        /// <returns>
        /// A collection of CategoryDto objects or
        /// HTTP 404 if no categories are found.
        /// </returns>
        [HttpGet(Name = "GetAllCategories")]
        public ActionResult<IEnumerable<CategoryDto>> Get()
        {
            // Retrieves all categories from the repository
            var categories = _uof.RepositoryCategory
                             .GetAll()
                             .OrderBy(c => c.Id)
                             .ToList();

            // Returns HTTP 404 if no categories exist
            if (categories.Count == 0)
            {
                return NotFound("No category was found.");
            }

            // Maps Category entities to CategoryDto objects
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            return Ok(categoriesDto);
        }

        /// <summary>
        /// Retrieves a category by its unique identifier.
        /// </summary>
        /// <param name="id">
        /// Unique identifier of the category.
        /// </param>
        /// <returns>
        /// Returns the requested CategoryDto if found;
        /// otherwise HTTP 404 Not Found.
        /// </returns>
        [HttpGet("{id:int:min(1)}", Name = "GetCategoryById")]
        public ActionResult<CategoryDto> Get([FromRoute] int id)
        {
            // Searches for the category using the provided identifier
            var category = _uof.RepositoryCategory.Get(c => c.Id == id);

            // Returns HTTP 404 if the category does not exist
            if (category is null)
            {
                return NotFound($"Category with Id={id} not found");
            }

            // Maps the Category entity to CategoryDto
            var categoryDto = _mapper.Map<CategoryDto>(category);

            return Ok(categoryDto);
        }


        /// <summary>
        /// Retrieves categories using pagination.
        /// 
        /// Pagination parameters are received through the query string,
        /// allowing clients to define the current page number
        /// and the number of items returned per page.
        /// 
        /// Example:
        /// GET /categories/pagination?pageNumber=1&pageSize=10
        /// </summary>
        /// <param name="queryParams">
        /// Object containing pagination parameters received from the URL query string.
        /// 
        /// PageNumber:
        /// Defines which page should be returned.
        /// 
        /// PageSize:
        /// Defines how many records should be returned per page.
        /// </param>
        /// <returns>
        /// A paginated collection of CategoryDto objects.
        /// 
        /// Pagination metadata is also included in the response headers
        /// using the "X-Pagination" custom header.
        /// </returns>
        [HttpGet("pagination", Name = "PaginationCategories")]
        public ActionResult<IEnumerable<CategoryDto>> Pagination([FromQuery] QueryParams queryParams)
        {
            // Retrieves all categories ordered by Id
            var items = _uof.RepositoryCategory
                             .GetAll()
                             .OrderBy(c => c.Id);

            // Creates a paginated list based on
            // the requested page number and page size
            var pagedList = PagedList<Category>.ToPagedList(
                items,
                queryParams.PageNumber,
                queryParams.PageSize
            );

            // Maps Category entities to CategoryDto objects
            var pagedListDto = _mapper.Map<IEnumerable<CategoryDto>>(pagedList);

            // Creates pagination metadata object
            // to provide additional information to the client
            var metadata = new
            {
                // Current page being returned
                pagedList.CurrentPage,

                // Number of items per page
                pagedList.PageSize,

                // Total number of records available
                pagedList.TotalCount,

                // Total number of pages available
                pagedList.TotalPages,

                // Indicates whether a next page exists
                pagedList.HasNext,

                // Indicates whether a previous page exists
                pagedList.HasPrevious,
            };

            // Adds pagination metadata to the response headers
            // as a serialized JSON string
            Response.Headers.Append(
                "X-Pagination",
                JsonSerializer.Serialize(metadata)
            );

            // Returns the paginated DTO collection
            return Ok(pagedListDto);
        }

        /// <summary>
        /// Retrieves categories filtered by name using pagination.
        /// 
        /// The filter value is received through query string parameters,
        /// allowing clients to search categories by name while also
        /// controlling pagination settings.
        /// 
        /// Example:
        /// GET /categories/filter/name?name=electronics&pageNumber=1&pageSize=5
        /// </summary>
        /// <param name="categoryFilterParams">
        /// Object containing:
        /// 
        /// - Name filter parameters
        /// - Pagination parameters
        /// </param>
        /// <returns>
        /// A paginated collection of CategoryDto objects
        /// including pagination metadata in the response headers.
        /// </returns>
        [HttpGet("filter/name", Name = "GetCategoriesByName")]
        public ActionResult<IEnumerable<CategoryDto>> GetCategoriesByName(
            [FromQuery] CategoryFilterParams categoryFilterParams)
        {
            // Retrieves filtered and paginated categories
            // based on the provided filter parameters
            var pagedList =
                _uof.RepositoryCategory
                    .GetCategoriesByNameFilter(categoryFilterParams);

            // Sends pagination metadata in the response header
            // and returns the paginated DTO collection
            return AddPaginationHeader(pagedList);
        }
        /// <summary>
        /// Adds pagination metadata to the HTTP response headers
        /// and returns the paginated DTO collection.
        /// 
        /// The metadata is serialized as JSON and stored
        /// in the custom "X-Pagination" response header.
        /// </summary>
        /// <param name="pagedList">
        /// Paginated collection containing Category entities
        /// and pagination metadata.
        /// </param>
        /// <returns>
        /// A paginated 
        private ActionResult<IEnumerable<CategoryDto>> AddPaginationHeader(PagedList<Category> pagedList)
        {
            // Creates an anonymous object containing
            // pagination metadata information
            var metadata = new
            {
                // Current page being returned
                pagedList.CurrentPage,

                // Number of items returned per page
                pagedList.PageSize,

                // Total number of records available
                pagedList.TotalCount,

                // Total number of available pages
                pagedList.TotalPages,

                // Indicates whether a next page exists
                pagedList.HasNext,

                // Indicates whether a previous page exists
                pagedList.HasPrevious,
            };

            // Adds pagination metadata to the response headers
            // as a serialized JSON string
            Response.Headers.Append(
                "X-Pagination",
                JsonSerializer.Serialize(metadata)
            );

            // Maps Category entities to CategoryDto objects
            var pagedListDto =
                _mapper.Map<IEnumerable<CategoryDto>>(pagedList);

            // Returns the paginated DTO collection
            return Ok(pagedListDto);
        }

        /// <summary>
        /// Creates a new category resource.
        /// </summary>
        /// <param name="categoryDto">
        /// CategoryDto object received from the request body.
        /// 
        /// This DTO contains the data required to create
        /// a new category resource.
        /// </param>
        /// <returns>
        /// Returns the created CategoryDto with HTTP 201 Created status.
        /// </returns>
        [HttpPost(Name = "CreateCategory")]
        public ActionResult<CategoryDto> Add([FromBody] CategoryDto categoryDto)
        {
            // Creates a new Category entity based on the CategoryDto
            var category = _mapper.Map<Category>(categoryDto);

            // Adds the new category to the repository
            var createdCategory = _uof.RepositoryCategory.Add(category);

            // Persists changes to the database
            _uof.Commit();

            // Maps the created entity to CategoryDto
            var createdCategoryDto = _mapper.Map<CategoryDto>(createdCategory);

            // Returns HTTP 201 Created with the route
            // to retrieve the newly created resource
            return new CreatedAtRouteResult(
                "GetCategoryById",
                new { id = createdCategoryDto.Id },
                createdCategoryDto
            );
        }

        /// <summary>
        /// Updates an existing category resource.
        /// </summary>
        /// <param name="id">
        /// Unique identifier of the category received from the route.
        /// </param>
        /// <param name="categoryDto">
        /// CategoryDto object containing the updated
        /// category data received from the request body.
        /// </param>
        /// <returns>
        /// Returns the updated CategoryDto or HTTP 400 Bad Request
        /// if the route Id does not match the DTO Id.
        /// </returns>
        [HttpPut("{id:int:min(1)}", Name = "UpdateCategory")]
        public ActionResult<CategoryDto> Update(int id, [FromBody] CategoryDto categoryDto)
        {
            // Validates whether the route Id matches
            // the DTO identifier
            if (id != categoryDto.Id)
            {
                return BadRequest("The ID in the URL is different from the category ID");
            }

            // Creates a Category entity based on the CategoryDto
            var category = _mapper.Map<Category>(categoryDto);

            // Updates the category entity
            var updatedCategory = _uof.RepositoryCategory.Update(category);

            // Persists changes to the database
            _uof.Commit();

            // Maps the updated entity to CategoryDto
            var updatedCategoryDto = _mapper.Map<CategoryDto>(updatedCategory);

            return Ok(updatedCategoryDto);
        }

        /// <summary>
        /// Deletes a category resource by its identifier.
        /// </summary>
        /// <param name="id">
        /// Unique identifier of the category.
        /// </param>
        /// <returns>
        /// Returns the deleted CategoryDto if the category exists;
        /// otherwise HTTP 404 Not Found.
        /// </returns>
        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<CategoryDto> Delete(int id)
        {
            // Searches for the category before deletion
            var category = _uof.RepositoryCategory.Get(c => c.Id == id);

            // Returns HTTP 404 if the category does not exist
            if (category is null)
            {
                return NotFound($"Category with Id={id} not found");
            }

            // Removes the category from the repository
            var deletedCategory = _uof.RepositoryCategory.Delete(category);

            // Persists changes to the database
            _uof.Commit();

            // Maps the deleted entity to CategoryDto
            var deletedCategoryDto = _mapper.Map<CategoryDto>(deletedCategory);

            return Ok(deletedCategoryDto);
        }
    }
}