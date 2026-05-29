using AutoMapper;
using Criando_Minha_Primeira_API.Model;

namespace Criando_Minha_Primeira_API.DTOs.Mapping
{
    /// <summary>
    /// AutoMapper profile responsible for configuring
    /// mappings between entities and DTOs.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes all application mapping configurations.
        /// </summary>
        public MappingProfile()
        {
            // Creates bidirectional mapping between Product and ProductDto
            CreateMap<Product, ProductDto>().ReverseMap();

            // Creates bidirectional mapping between Category and CategoryDto
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}