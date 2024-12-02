using AutoMapper;
using Infrastructure.Mapping;
using Product.API.Entities;
using Shared.Dtos;

namespace Product.API.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CatalogProduct, ProductDto>();
        CreateMap<CatalogProduct, CreateProductDto>()
            .ReverseMap(); // Allow two ways mapping

        CreateMap<UpdateProductDto,CatalogProduct >()
            .IgnoreAllProperties(); // From Infrastructure project, Mapping/AutoMapperExtension.cs
    }
}