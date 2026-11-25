using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moto_Taxi_Maintenance_Tracker.Data;
using Moto_Taxi_Maintenance_Tracker.Models;

namespace Moto_Taxi_Maintenance_Tracker.Pages.Motorcycles;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Motorcycle Motorcycle { get; set; } = new();

    public SelectList OwnerList { get; set; } = default!;

    public async Task OnGetAsync()
    {
        await PopulateOwnersAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (await _context.Motorcycles.AnyAsync(m => m.PlateNumber == Motorcycle.PlateNumber))
        {
            ModelState.AddModelError(nameof(Motorcycle.PlateNumber), "A motorcycle with this plate number already exists.");
        }

        if (!ModelState.IsValid)
        {
            await PopulateOwnersAsync();
            return Page();
        }

        _context.Motorcycles.Add(Motorcycle);
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }

    private async Task PopulateOwnersAsync()
    {
        var owners = await _context.Users
            .Where(u => u.Role == UserRole.Owner)
            .OrderBy(u => u.FullName)
            .ToListAsync();

        OwnerList = new SelectList(owners, nameof(Models.User.Id), nameof(Models.User.FullName));
    }
}
