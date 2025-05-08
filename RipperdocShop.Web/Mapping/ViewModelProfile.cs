using AutoMapper;
using RipperdocShop.Shared.DTOs;
using RipperdocShop.Web.Models.Auth;
using RipperdocShop.Web.Models.ViewModels;

namespace RipperdocShop.Web.Mapping;

public class ViewModelProfile : Profile
{
    public ViewModelProfile()
    {
        CreateMap<LoginViewModel, LoginDto>();
        CreateMap<RegisterViewModel, LoginDto>();
        
    }
}
