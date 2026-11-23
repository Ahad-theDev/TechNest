using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechNest.Models;

namespace TechNest.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var model = new UserHomeViewModel
        {
            FullName = string.IsNullOrWhiteSpace(user.FullName) ? user.Email ?? "" : user.FullName,
            Email = user.Email ?? "",
            Roles = string.Join(", ", await _userManager.GetRolesAsync(user))
        };

        return View(model);
    }
}