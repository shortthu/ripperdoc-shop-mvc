using AutoMapper;
using RipperdocShop.Api.Models.Entities;
using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Api.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryCreateDto>();
        CreateMap<Brand, BrandCreateDto>();
        CreateMap<Brand, BrandDto>();
        CreateMap<ProductRating, ProductRatingDto>();
        CreateMap<ProductRating, ProductRatingCreateDto>();
    }
}
