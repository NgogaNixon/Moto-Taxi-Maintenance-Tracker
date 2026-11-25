using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moto_Taxi_Maintenance_Tracker.Data;
using Moto_Taxi_Maintenance_Tracker.Models;

namespace Moto_Taxi_Maintenance_Tracker.Pages.Garages;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Garage Garage { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var garage = await _context.Garages.FindAsync(id);
        if (garage is null)
        {
            return NotFound();
        }

        Garage = garage;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var garage = await _context.Garages.FindAsync(id);
        if (garage is not null)
        {
            _context.Garages.Remove(garage);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("Index");
    }
}
