using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechNest.Models;

namespace TechNest.Controllers;

[Authorize(Roles = "Customer")]
public class CustomerController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public CustomerController(UserManager<IdentityUser> userManager)
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
            Email = user.Email ?? "",
            Roles = string.Join(", ", await _userManager.GetRolesAsync(user))
        };

        return View(model);
    }
}