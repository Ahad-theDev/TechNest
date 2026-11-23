using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechNest.Models;
using TechNest.Repositories;

namespace TechNest.Controllers;

[Authorize(Roles ="Admin")]
public class CategoriesController : Controller
{
    private readonly ICategoryRepository _categories;

    public CategoriesController(ICategoryRepository categories)
    {
        _categories = categories;
    }

    public async Task<IActionResult> Index()
    {
        var list = await _categories.GetAllAsync();
        return View(list.ToList());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        if(!ModelState.IsValid)
        {
            return View(category);
        }
        await _categories.AddAsync(category);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _categories.DeleteAsync(id);
        return Json(new {sucess = deleted});
    }
}