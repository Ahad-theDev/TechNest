using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TechNest.Models;
using TechNest.Repositories;

namespace TechNest.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICategoryRepository _categories;

    public HomeController(ILogger<HomeController> logger,ICategoryRepository categories)
    {
        _logger = logger;
        _categories = categories;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categories.GetAllAsync();
        return View(categories.ToList());
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration =0,Location =ResponseCacheLocation.None,NoStore =true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel{RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}