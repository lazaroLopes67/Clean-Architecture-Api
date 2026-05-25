using Criando_Minha_Primeira_API.Model;
using Criando_Minha_Primeira_API.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace Criando_Minha_Primeira_API.Controllers
{
    /// <summary>
    /// API controller responsible for managing Category resources.
    /// Provides endpoints for CRUD operations using Unit of Work pattern.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        /// <summary>
        /// Unit of Work instance used to access repositories and commit changes.
        /// </summary>
        private readonly IUnitOfWork _uof;

        /// <summary>
        /// Initializes a new instance of the CategoriesController.
        /// </summary>
        /// <param name="uof">Unit of Work injected via dependency injection.</param>
        public CategoriesController(IUnitOfWork uof)
        {
            _uof = uof;
        }
        /// <summary>
        /// Retrieves all categories ordered by Id.
        /// </summary>
        /// <returns>A list of categories or 404 if none are found.</returns>
        [HttpGet(Name = "GetAllCategories")]
        public ActionResult<IEnumerable<Category>> Get()
        {
            var items = _uof.RepositoryCategory
                             .GetAll()
                             .OrderBy(c => c.Id)
                             .ToList();

            if (items is null || items.Count == 0)
            {
                return NotFound("No category was found.");
            }

            return Ok(items);
        }
        /// <summary>
        /// Retrieves a category by its unique identifier.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <returns>The category if found; otherwise, 404 Not Found.</returns>
        [HttpGet("{id:int:min(1)}", Name = "GetCategoryById")]
        public ActionResult<Category> Get([FromRoute] int id)
        {
            var item = _uof.RepositoryCategory.Get(c => c.Id == id);

            if (item is null)
            {
                return NotFound($"Category with Id={id} not found");
            }

            return Ok(item);
        }

        /// <summary>
        /// Creates a new category in the database.
        /// </summary>
        /// <param name="category">Category object sent in the request body.</param>
        /// <returns>The created category with its generated Id.</returns>
        [HttpPost(Name = "CreateCategory")]
        public ActionResult<Category> Add([FromBody] Category category)
        {
            var created_category = _uof.RepositoryCategory.Add(category);
            _uof.Commit();

            return new CreatedAtRouteResult(
                "GetCategoryById",
                new { id = created_category.Id },
                created_category
            );
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="id">Category Id from the route.</param>
        /// <param name="category">Updated category data.</param>
        /// <returns>The updated category.</returns>
        [HttpPut("{id:int:min(1)}", Name = "UpdateCategory")]
        public ActionResult<Category> Update(int id, [FromBody] Category category)
        {
            if (id != category.Id)
                return BadRequest("The ID in the URL is different from the category ID");

            var updated_category = _uof.RepositoryCategory.Update(category);
            _uof.Commit();

            return Ok(updated_category);
        }

        /// <summary>
        /// Deletes a category by its Id.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <returns>The deleted category or 404 if not found.</returns>
        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<Category> Delete(int id)
        {
            var category = _uof.RepositoryCategory.Get(c => c.Id == id);

            if (category is not null)
            {
                var deleted_category = _uof.RepositoryCategory.Delete(category);
                _uof.Commit();
                return Ok(deleted_category);
            }

            return NotFound($"Category with Id={id} not found");
        }
    }
}