using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Moto_Taxi_Maintenance_Tracker.Data;
using Moto_Taxi_Maintenance_Tracker.Models;

namespace Moto_Taxi_Maintenance_Tracker.Pages.Garages;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
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

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Garage).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Garages.AnyAsync(g => g.Id == Garage.Id))
            {
                return NotFound();
            }
            throw;
        }

        return RedirectToPage("Index");
    }
}
