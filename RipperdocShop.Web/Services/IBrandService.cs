using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Web.Services;

public interface IBrandService
{
    Task<BrandDto?> GetBySlugAsync(string slug);
    Task<(List<BrandDto> Products, int TotalCount, int TotalPages)> GetAllAsync();
}
