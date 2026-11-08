using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechNest.Controllers;

[Authorize(Roles = "Customer")]
public class CustomerController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}