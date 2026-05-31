using Criando_Minha_Primeira_API.Model;
using Criando_Minha_Primeira_API.Pagination;

namespace Criando_Minha_Primeira_API.Repositories.Interfaces
{
    /// <summary>
    /// Defines a specialized repository contract for Category entities.
    /// 
    /// Inherits generic CRUD operations from IRepository
    /// and adds Category-specific query methods.
    /// </summary>
    public interface ICategoryRepository : IRepository<Category>
    {
        /// <summary>
        /// Retrieves categories filtered by name
        /// with pagination support.
        /// 
        /// The filtering behavior is based on
        /// the values provided in CategoryFilterParams.
        /// </summary>
        /// <param name="categoryFilterParams">
        /// Object containing:
        /// 
        /// - Category name filter parameters
        /// - Pagination settings such as page number and page size
        /// </param>
        /// <returns>
        /// A paginated collection of Category entities
        /// matching the provided filter criteria.
        /// </returns>
        PagedList<Category> GetCategoriesByNameFilter(
            CategoryFilterParams categoryFilterParams
        );
    }
}