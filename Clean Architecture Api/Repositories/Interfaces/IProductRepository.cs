using Criando_Minha_Primeira_API.Model;
using Criando_Minha_Primeira_API.Pagination;

namespace Criando_Minha_Primeira_API.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for Product repository operations.
    /// 
    /// This interface extends the generic IRepository
    /// and contains Product-specific queries and business rules.
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Retrieves a paginated collection of products
        /// filtered by price range parameters.
        /// 
        /// The filtering rules are defined by the
        /// ProductFilterParams object.
        /// </summary>
        /// <param name="productFilterParams">
        /// Object containing filtering and pagination parameters,
        /// such as:
        /// 
        /// - Minimum price
        /// - Maximum price
        /// - Page number
        /// - Page size
        /// </param>
        /// <returns>
        /// A paginated list of Product entities
        /// matching the specified price filter.
        /// </returns>
        PagedList<Product> GetProductsByPriceFilter(ProductFilterParams productFilterParams);
    }
}