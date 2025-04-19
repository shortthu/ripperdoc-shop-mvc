using AutoMapper;
using RipperdocShop.Models.Entities;
using RipperdocShop.Models.ViewModels;

namespace RipperdocShop.Models.Mappings;

public class CategoriesProfile : Profile
{
    public CategoriesProfile()
    {
        // Don't touch these fields
        CreateMap<Category, CategoryViewModel>().ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Slug, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());
    }
}