using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Web.Services;

public interface IBrandService
{
    Task<BrandDto?> GetBySlugAsync(string slug);
    Task<BrandResponseDto?> GetAllAsync();
}
