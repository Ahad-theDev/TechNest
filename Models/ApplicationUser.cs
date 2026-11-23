using Microsoft.AspNetCore.Identity;

namespace TechNest.Models;

public class ApplicationUser : IdentityUser
{
    [PersonalData]
    public string FullName { get; set; } = "";

    [PersonalData]
    public string? Address { get; set; }
}