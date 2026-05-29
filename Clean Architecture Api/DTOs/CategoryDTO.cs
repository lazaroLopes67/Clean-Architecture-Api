namespace Criando_Minha_Primeira_API.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) used to expose
    /// category data through the API.
    /// 
    /// DTOs help control which properties are sent
    /// to the client and avoid exposing the entire entity model.
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// Unique identifier of the category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the category.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// URL of the image associated with the category.
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;
    }
}