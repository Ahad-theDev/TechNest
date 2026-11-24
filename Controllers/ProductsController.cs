using Microsoft.AspNetCore.Mvc;
using TechNest.Repositories;

namespace TechNest.Controllers;

public class ProductsController : Controller
{
    private readonly IProductRepository _products;

    public ProductsController(IProductRepository products)
    {
        _products = products;
    }

    public async Task<IActionResult> Index()
    {
        var list = await _products.SearchAsync("");
        return View(list.ToList());
    }

    public async Task<IActionResult> Search(string term)
    {
        var list = await _products.SearchAsync(term ?? "");
        return PartialView("_ProductCards",list.ToList());
    }
}