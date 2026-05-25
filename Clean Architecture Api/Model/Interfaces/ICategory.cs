namespace Criando_Minha_Primeira_API.Model.Interfaces
{
    /// <summary>
    /// Defines the structure of a category entity
    /// </summary>
    public interface ICategory
    {
        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the category image URL.
        /// </summary>
        string ImageUrl { get; set; }
        /// <summary>
        /// Gets or sets the products associated with the category.
        /// </summary>
        ICollection<Product>? Products { get; set; }
    }
}
