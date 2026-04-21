using ECommerceWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Areas.Identity.Pages.Account;

public class LoginModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LoginModel(SignInManager<ApplicationUser> signInManager)
        => _signInManager = signInManager;

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var result = await _signInManager.PasswordSignInAsync(
            Input.Email, Input.Password, false, false);

        if (result.Succeeded)
            return RedirectToAction("Index", "Home", new { area = "Customer" });

        ModelState.AddModelError("", "Email o contraseña incorrectos");
        return Page();
    }
}