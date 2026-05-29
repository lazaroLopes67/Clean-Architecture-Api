namespace Criando_Minha_Primeira_API.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) used to expose
    /// product information through the API.
    /// 
    /// DTOs are commonly used to transfer only the necessary
    /// data to the client while hiding internal entity details.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Unique identifier of the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Product price.
        /// Uses decimal type to ensure precision in monetary values.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Short description of the product.
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Represents the available quantity of the product in stock.
        /// 
        /// This property is used for inventory control and stock management.
        /// </summary>
        public decimal Stock { get; set; }

        /// <summary>
        /// Represents the identifier of the category
        /// associated with the product.
        /// 
        /// This property is used to establish the relationship
        /// between Product and Category entities.
        /// </summary>
        public int CategoryId { get; set; }
    }
}