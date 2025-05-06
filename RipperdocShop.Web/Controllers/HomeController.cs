using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RipperdocShop.Web.Models;
using RipperdocShop.Web.Services;

namespace RipperdocShop.Web.Controllers;

public class HomeController(ILogger<HomeController> logger, IProductService productService) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
