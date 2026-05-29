using AutoMapper;
using Criando_Minha_Primeira_API.DTOs;
using Criando_Minha_Primeira_API.Model;
using Criando_Minha_Primeira_API.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

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