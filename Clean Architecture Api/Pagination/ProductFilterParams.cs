namespace Criando_Minha_Primeira_API.Pagination
{
    /// <summary>
    /// Represents filtering and pagination parameters
    /// used for product queries.
    /// 
    /// This class extends QueryParams,
    /// inheriting pagination features such as:
    /// 
    /// - PageNumber
    /// - PageSize
    /// 
    /// It also adds product-specific filtering options.
    /// </summary>
    public class ProductFilterParams : QueryParams
    {
        /// <summary>
        /// Represents the price value used
        /// in the product filtering operation.
        /// 
        /// This property can be null,
        /// meaning no price filter was provided.
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Defines the filtering criterion
        /// applied to the price comparison.
        /// 
        /// Example values:
        /// 
        /// - "greater"
        /// - "less"
        /// - "equal"
        /// 
        /// This property determines how the Price value
        /// should be interpreted during filtering.
        /// </summary>
        public string? Criterion { get; set; }
    }
}