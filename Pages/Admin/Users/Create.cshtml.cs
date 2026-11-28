using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Moto_Taxi_Maintenance_Tracker.Data;
using Moto_Taxi_Maintenance_Tracker.Models;
using System.ComponentModel.DataAnnotations;

namespace Moto_Taxi_Maintenance_Tracker.Pages.Admin.Users;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher = new();

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public UserInput Input { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (await _context.Users.AnyAsync(u => u.Email == Input.Email))
        {
            ModelState.AddModelError($"{nameof(Input)}.{nameof(Input.Email)}", "A user with this email already exists.");
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = new User
        {
            FullName = Input.FullName,
            Email = Input.Email,
            Role = Input.Role,
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, Input.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }

    public class UserInput
    {
        [Required, StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress, StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; } = UserRole.Owner;
    }
}
