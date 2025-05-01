namespace RipperdocShop.Api.Services.Core;

public interface IImageService
{
    Task<string> UploadImageAsync(IFormFile image, string folder = "images");
}
