using AutoMapper;
using Shared.DTOs;

namespace Product.API.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Entities.Product, ProductDto>();
            CreateMap<CreateProductDto, Entities.Product>();
            CreateMap<UpdateProductDto, Entities.Product>();
        }
    }
}
