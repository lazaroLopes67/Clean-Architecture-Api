using Criando_Minha_Primeira_API.Context;
using Criando_Minha_Primeira_API.Model;
using Criando_Minha_Primeira_API.Pagination;
using Criando_Minha_Primeira_API.Repositories.Interfaces;

namespace Criando_Minha_Primeira_API.Repositories
{
    /// <summary>
    /// Repository responsible for Product-specific data access operations.
    /// 
    /// This class extends the generic Repository implementation
    /// and adds custom filtering logic related to Product entities.
    /// </summary>
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        /// <summary>
        /// Initializes a new instance of the ProductRepository class.
        /// </summary>
        /// <param name="context">
        /// Database context used to access Product entities.
        /// </param>
        public ProductRepository(AppDbContext context) : base(context)
        {

        }

        /// <summary>
        /// Retrieves a paginated collection of products
        /// filtered by price criteria.
        /// 
        /// The filtering behavior depends on the Criterion property:
        /// 
        /// - "greater" : returns products with price greater than the provided value
        /// - "smaller" : returns products with price smaller than the provided value
        /// - "equal"   : returns products with price equal to the provided value
        /// 
        /// If no valid filter is provided,
        /// all products are returned using pagination.
        /// </summary>
        /// <param name="productFilterParams">
        /// Object containing pagination and filtering parameters.
        /// </param>
        /// <returns>
        /// A paginated list of Product entities
        /// matching the specified filter criteria.
        /// </returns>
        public PagedList<Product> GetProductsByPriceFilter(ProductFilterParams productFilterParams)
        {
            // Retrieves all products as a queryable collection
            // to allow dynamic filtering and pagination
            var products = GetAll().AsQueryable();

            // Validates whether a price filter
            // and a filtering criterion were provided
            if (productFilterParams.Price.HasValue &&
               !string.IsNullOrEmpty(productFilterParams.Criterion))
            {
                // Filters products with price greater
                // than the specified value
                if (productFilterParams.Criterion.Equals(
                    "greater",
                    StringComparison.OrdinalIgnoreCase))
                {
                    products = products
                        .Where(p => p.Price > productFilterParams.Price);
                }

                // Filters products with price less
                // than the specified value
                else if (productFilterParams.Criterion.Equals(
                    "less",
                    StringComparison.OrdinalIgnoreCase))
                {
                    products = products
                        .Where(p => p.Price < productFilterParams.Price);
                }

                // Filters products with price equal
                // to the specified value
                else if (productFilterParams.Criterion.Equals(
                    "equal",
                    StringComparison.OrdinalIgnoreCase))
                {
                    products = products
                        .Where(p => p.Price == productFilterParams.Price);
                }
                products.OrderBy(p => p.Id);
                // Returns the filtered products
                // using pagination
                return PagedList<Product>.ToPagedList(
                    products,
                    productFilterParams.PageNumber,
                    productFilterParams.PageSize
                );
            }

            // Returns all products using pagination
            // when no filter is provided
            return PagedList<Product>.ToPagedList(
                products,
                productFilterParams.PageNumber,
                productFilterParams.PageSize
            );
        }
    }
}