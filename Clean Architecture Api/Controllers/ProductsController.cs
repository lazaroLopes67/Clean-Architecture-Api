using Criando_Minha_Primeira_API.Model;
using Criando_Minha_Primeira_API.Model.Interfaces;
using Criando_Minha_Primeira_API.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace Criando_Minha_Primeira_API.Controllers
{
    /// <summary>
    /// API controller responsible for managing Procuct resources.
    /// Provides endpoints for CRUD operations using Unit of Work pattern.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// Unit of Work instance used to access repositories and commit changes.
        /// </summary>
        private readonly IUnitOfWork _uof;

        /// <summary>
        /// Initializes a new instance of the ProductsController.
        /// </summary>
        /// <param name="uof">Unit of Work injected via dependency injection.</param>
        public ProductsController(IUnitOfWork uof)
        {
            _uof = uof;
        }
        /// <summary>
        /// Retrieves all products ordered by Id.
        /// </summary>
        /// <returns>A list of product or 404 if none are found.</returns>
        [HttpGet(Name = "GetAllProcts")]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var items = _uof.RepositoryProducts
                             .GetAll()
                             .OrderBy(p => p.Id)
                             .ToList();

            if (items is null || items.Count == 0)
            {
                return NotFound("No product was found.");
            }

            return Ok(items);
        }
        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <returns>The product if found; otherwise, 404 Not Found.</returns>
        [HttpGet("{id:int:min(1)}", Name = "GetProductById")]
        public ActionResult<Product> Get([FromRoute] int id)
        {
            var item = _uof.RepositoryProducts.Get(p => p.Id == id);

            if (item is null)
            {
                return NotFound($"Product with Id={id} not found");
            }

            return Ok(item);
        }

        /// <summary>
        /// Creates a new product in the database.
        /// </summary>
        /// <param name="category">Product object sent in the request body.</param>
        /// <returns>The created product with its generated Id.</returns>
        [HttpPost(Name = "CreateProduct")]
        public ActionResult<Product> Add([FromBody] Product product)
        {
            var created_product = _uof.RepositoryProducts.Add(product);
            _uof.Commit();
            return new CreatedAtRouteResult(
                "GetProductById",
                new { id = created_product.Id },
                created_product
            );
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">Product Id from the route.</param>
        /// <param name="category">Updated product data.</param>
        /// <returns>The updated product.</returns>
        [HttpPut("{id:int:min(1)}", Name = "UpdateProduct")]
        public ActionResult<Product> Update(int id, [FromBody] Product product)
        {
            if (id != product.Id)
                return BadRequest("The ID in the URL is different from the product ID");

            var updated_product = _uof.RepositoryProducts.Update(product);
            _uof.Commit();

            return Ok(updated_product);
        }

        /// <summary>
        /// Deletes a Product by its Id.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <returns>The deleted product or 404 if not found.</returns>
        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<Product> Delete(int id)
        {
            var product = _uof.RepositoryCategory.Get(P => P.Id == id);

            if (product is not null)
            {
                var deleted_product = _uof.RepositoryCategory.Delete(product);
                _uof.Commit();
                return Ok(deleted_product);
            }

            return NotFound($"Product with Id={id} not found");
        }
    }
}