using Criando_Minha_Primeira_API.Model.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Criando_Minha_Primeira_API.Model;
/// <summary>
/// Represents a product category in the application.
/// Stores category information such as name, image,
/// and the products associated with the category.
/// </summary>
public class Category : ICategory
{
    public Category() => Products = new Collection<Product>();
    /// <summary>
    /// Gets or sets the unique identifier of the category.
    /// </summary>
    [Key]
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the category name.
    /// The name must contain between 3 and 100 characters.
    /// </summary>
    [Required(ErrorMessage = "The category name is required.")]
    [StringLength(
        100,
        MinimumLength = 3,
        ErrorMessage = "The name must contain between 3 and 100 characters.")]
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the image URL representing the category.
    /// </summary>
    [Required(ErrorMessage = "The category image URL is required.")]
    [StringLength(200)]
    public string ImageUrl { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the collection of products
    /// associated with the category.
    /// </summary>
    [JsonIgnore]
    public ICollection<Product>? Products { get; set; }
}