namespace Criando_Minha_Primeira_API.Model.Interfaces
{
    /// <summary>
    /// Defines the structure of a product entity.
    /// Represents product information such as name,
    /// description, stock quantity, image, and category association.
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// Gets the unique identifier of the product.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the product description.
        /// </summary>
        string? Description { get; set; }

        /// <summary>
        /// Gets or sets the product image URL.
        /// </summary>
        string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the available stock quantity of the product.
        /// </summary>
        decimal Stock { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the category
        /// associated with the product.
        /// </summary>
        int? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category associated with the product.
        /// </summary>
        Category? Category { get; set; }
    }
}
