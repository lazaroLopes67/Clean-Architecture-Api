using AutoMapper;
using Criando_Minha_Primeira_API.DTOs;
using Criando_Minha_Primeira_API.Model;
using Criando_Minha_Primeira_API.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace Criando_Minha_Primeira_API.Controllers
{
    /// <summary>
    /// API controller responsible for managing product resources.
    /// 
    /// This controller exposes CRUD endpoints for products
    /// and uses DTOs to control the data exposed to API clients.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
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
        /// Initializes a new instance of the ProductsController class.
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
        public ProductsController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all products ordered by their Id.
        /// 
        /// The retrieved entities are mapped to ProductDto objects
        /// before being returned to the client.
        /// </summary>
        /// <returns>
        /// A collection of ProductDto objects or
        /// HTTP 404 if no products are found.
        /// </returns>
        [HttpGet(Name = "GetAllProducts")]
        public ActionResult<IEnumerable<ProductDto>> Get()
        {
            // Retrieves all products from the repository
            var products = _uof.RepositoryProducts
                             .GetAll()
                             .OrderBy(p => p.Id)
                             .ToList();

            // Returns HTTP 404 if no products exist
            if (products.Count == 0)
            {
                return NotFound("No product was found.");
            }

            // Maps Product entities to ProductDto objects
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(productsDto);
        }

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">
        /// Unique identifier of the product.
        /// </param>
        /// <returns>
        /// Returns the requested ProductDto if found;
        /// otherwise HTTP 404 Not Found.
        /// </returns>
        [HttpGet("{id:int:min(1)}", Name = "GetProductById")]
        public ActionResult<ProductDto> Get([FromRoute] int id)
        {
            // Searches for the product using the provided identifier
            var product = _uof.RepositoryProducts.Get(p => p.Id == id);

            // Returns HTTP 404 if the product does not exist
            if (product is null)
            {
                return NotFound($"Product with Id={id} not found");
            }

            // Maps the Product entity to ProductDto
            var productDto = _mapper.Map<ProductDto>(product);

            return Ok(productDto);
        }

        /// <summary>
        /// Creates a new product resource.
        /// </summary>
        /// <param name="productDto">
        /// ProductDto object received from the request body.
        /// 
        /// This DTO contains the data required to create
        /// a new product resource.
        /// </param>
        /// <returns>
        /// Returns the created ProductDto with HTTP 201 Created status.
        /// </returns>
        [HttpPost(Name = "CreateProduct")]
        public ActionResult<ProductDto> Add([FromBody] ProductDto productDto)
        {
            // Creates a new Product entity based on the ProductDto
            var product = _mapper.Map<Product>(productDto);

            // Adds the new product to the repository
            var createdProduct = _uof.RepositoryProducts.Add(product);

            // Persists changes to the database
            _uof.Commit();

            // Maps the created entity to ProductDto
            var createdProductDto = _mapper.Map<ProductDto>(createdProduct);

            // Returns HTTP 201 Created with the route
            // to retrieve the newly created resource
            return new CreatedAtRouteResult(
                "GetProductById",
                new { id = createdProductDto.Id },
                createdProductDto
            );
        }

        /// <summary>
        /// Updates an existing product resource.
        /// </summary>
        /// <param name="id">
        /// Unique identifier of the product received from the route.
        /// </param>
        /// <param name="productDto">
        /// ProductDto object containing the updated
        /// product data received from the request body.
        /// </param>
        /// <returns>
        /// Returns the updated ProductDto or HTTP 400 Bad Request
        /// if the route Id does not match the DTO Id.
        /// </returns>
        [HttpPut("{id:int:min(1)}", Name = "UpdateProduct")]
        public ActionResult<ProductDto> Update(int id, [FromBody] ProductDto productDto)
        {
            // Validates whether the route Id matches
            // the DTO identifier
            if (id != productDto.Id)
            {
                return BadRequest("The ID in the URL is different from the product ID");
            }

            // Creates a Product entity based on the ProductDto
            var product = _mapper.Map<Product>(productDto);

            // Updates the product entity
            var updatedProduct = _uof.RepositoryProducts.Update(product);

            // Persists changes to the database
            _uof.Commit();

            // Maps the updated entity to ProductDto
            var updatedProductDto = _mapper.Map<ProductDto>(updatedProduct);

            return Ok(updatedProductDto);
        }

        /// <summary>
        /// Deletes a product resource by its identifier.
        /// </summary>
        /// <param name="id">
        /// Unique identifier of the product.
        /// </param>
        /// <returns>
        /// Returns the deleted ProductDto if the product exists;
        /// otherwise HTTP 404 Not Found.
        /// </returns>
        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<ProductDto> Delete(int id)
        {
            // Searches for the product before deletion
            var product = _uof.RepositoryProducts.Get(p => p.Id == id);

            // Returns HTTP 404 if the product does not exist
            if (product is null)
            {
                return NotFound($"Product with Id={id} not found");
            }

            // Removes the product from the repository
            var deletedProduct = _uof.RepositoryProducts.Delete(product);

            // Persists changes to the database
            _uof.Commit();

            // Maps the deleted entity to ProductDto
            var deletedProductDto = _mapper.Map<ProductDto>(deletedProduct);

            return Ok(deletedProductDto);
        }
    }
}