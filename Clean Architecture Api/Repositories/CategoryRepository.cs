using Criando_Minha_Primeira_API.Context;
using Criando_Minha_Primeira_API.Model;
using Criando_Minha_Primeira_API.Pagination;
using Criando_Minha_Primeira_API.Repositories.Interfaces;

namespace Criando_Minha_Primeira_API.Repositories
{
    /// <summary>
    /// Specialized repository responsible for Category entity operations.
    /// 
    /// Inherits generic CRUD functionality from Repository<Category>
    /// and implements additional Category-specific queries.
    /// </summary>
    public class CategoryRepository
        : Repository<Category>, ICategoryRepository
    {
        /// <summary>
        /// Initializes a new instance of the CategoryRepository class.
        /// </summary>
        /// <param name="context">
        /// Database context used to access Category entities.
        /// </param>
        public CategoryRepository(AppDbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Retrieves categories filtered by name
        /// with pagination support.
        /// 
        /// If a name filter is provided,
        /// only categories containing the specified text
        /// in their Name property will be returned.
        /// </summary>
        /// <param name="categoryFilterParams">
        /// Object containing:
        /// 
        /// - Name filter parameters
        /// - Pagination settings such as page number and page size
        /// </param>
        /// <returns>
        /// A paginated collection of filtered Category entities.
        /// </returns>
        public PagedList<Category> GetCategoriesByNameFilter(
            CategoryFilterParams categoryFilterParams)
        {
            // Retrieves all categories as a queryable source
            // to allow dynamic filtering and pagination
            var categories = GetAll().AsQueryable();

            // Applies name filtering if the Name parameter was provided
            if (!string.IsNullOrWhiteSpace(categoryFilterParams.Name))
            {
                categories = categories.Where(
                    c => c.Name.Contains(categoryFilterParams.Name)
                );
            }
            // Orders categories by Id before pagination
            // to ensure consistent and predictable results
            // across paginated requests
            categories = categories.OrderBy(c => c.Id);            
            // Creates a paginated list based on the filtered query
            var filteredCategories =
                PagedList<Category>.ToPagedList(
                    categories,
                    categoryFilterParams.PageNumber,
                    categoryFilterParams.PageSize
                );
            
            return filteredCategories;
        }
    }
}