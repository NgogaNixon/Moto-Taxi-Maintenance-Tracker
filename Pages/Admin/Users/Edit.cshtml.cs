using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Moto_Taxi_Maintenance_Tracker.Data;
using Moto_Taxi_Maintenance_Tracker.Models;
using System.ComponentModel.DataAnnotations;

namespace Moto_Taxi_Maintenance_Tracker.Pages.Admin.Users;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher = new();

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public UserInput Input { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        Input = new UserInput
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role,
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _context.Users.FindAsync(Input.Id);
        if (user is null)
        {
            return NotFound();
        }

        if (await _context.Users.AnyAsync(u => u.Email == Input.Email && u.Id != Input.Id))
        {
            ModelState.AddModelError($"{nameof(Input)}.{nameof(Input.Email)}", "A user with this email already exists.");
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        user.FullName = Input.FullName;
        user.Email = Input.Email;
        user.Role = Input.Role;

        if (!string.IsNullOrWhiteSpace(Input.NewPassword))
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, Input.NewPassword);
        }

        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }

    public class UserInput
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress, StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Password), StringLength(100, MinimumLength = 6)]
        [Display(Name = "New Password (leave blank to keep current)")]
        public string? NewPassword { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
