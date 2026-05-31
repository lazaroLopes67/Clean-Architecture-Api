namespace Criando_Minha_Primeira_API.Pagination
{
    /// <summary>
    /// Represents filtering and pagination parameters
    /// used when querying Category resources.
    /// 
    /// Inherits pagination settings from QueryParams
    /// and adds Category-specific filtering options.
    /// </summary>
    public class CategoryFilterParams : QueryParams
    {
        /// <summary>
        /// Category name used for filtering.
        /// 
        /// When provided, only categories whose names
        /// contain the specified value will be returned.
        /// </summary>
        public string? Name { get; set; }
    }
}