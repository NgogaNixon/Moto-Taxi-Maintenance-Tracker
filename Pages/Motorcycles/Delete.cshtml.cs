using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Moto_Taxi_Maintenance_Tracker.Data;
using Moto_Taxi_Maintenance_Tracker.Models;

namespace Moto_Taxi_Maintenance_Tracker.Pages.Motorcycles;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Motorcycle Motorcycle { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var motorcycle = await _context.Motorcycles
            .Include(m => m.Owner)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (motorcycle is null)
        {
            return NotFound();
        }

        Motorcycle = motorcycle;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var motorcycle = await _context.Motorcycles.FindAsync(id);
        if (motorcycle is not null)
        {
            _context.Motorcycles.Remove(motorcycle);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("Index");
    }
}
