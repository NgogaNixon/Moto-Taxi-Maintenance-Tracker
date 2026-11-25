using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moto_Taxi_Maintenance_Tracker.Data;
using Moto_Taxi_Maintenance_Tracker.Models;

namespace Moto_Taxi_Maintenance_Tracker.Pages.Motorcycles;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Motorcycle Motorcycle { get; set; } = default!;

    public SelectList OwnerList { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var motorcycle = await _context.Motorcycles.FindAsync(id);
        if (motorcycle is null)
        {
            return NotFound();
        }

        Motorcycle = motorcycle;
        await PopulateOwnersAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (await _context.Motorcycles.AnyAsync(m => m.PlateNumber == Motorcycle.PlateNumber && m.Id != Motorcycle.Id))
        {
            ModelState.AddModelError(nameof(Motorcycle.PlateNumber), "A motorcycle with this plate number already exists.");
        }

        if (!ModelState.IsValid)
        {
            await PopulateOwnersAsync();
            return Page();
        }

        _context.Attach(Motorcycle).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Motorcycles.AnyAsync(m => m.Id == Motorcycle.Id))
            {
                return NotFound();
            }
            throw;
        }

        return RedirectToPage("Index");
    }

    private async Task PopulateOwnersAsync()
    {
        var owners = await _context.Users
            .Where(u => u.Role == UserRole.Owner)
            .OrderBy(u => u.FullName)
            .ToListAsync();

        OwnerList = new SelectList(owners, nameof(User.Id), nameof(User.FullName), Motorcycle.OwnerId);
    }
}
