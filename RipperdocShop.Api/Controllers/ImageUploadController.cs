using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RipperdocShop.Api.Services.Core;

namespace RipperdocShop.Api.Controllers;

[Route("api/admin/images")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
public class ImageUploadController(IImageService imageService) : ControllerBase
{
    [HttpPost("upload")]
    [RequestSizeLimit(ImageService.MaxFileSize)]
    public async Task<IActionResult> Upload(IFormFile image)
    {
        try
        {
            var imageUrl = await imageService.UploadImageAsync(image);
            return Ok(new { imageUrl });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
