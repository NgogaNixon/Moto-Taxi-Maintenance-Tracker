using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Moto_Taxi_Maintenance_Tracker.Data;
using Moto_Taxi_Maintenance_Tracker.Models;

namespace Moto_Taxi_Maintenance_Tracker.Pages.Motorcycles;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Motorcycle> Motorcycles { get; set; } = new List<Motorcycle>();

    [BindProperty(SupportsGet = true)]
    public string? SearchPlate { get; set; }

    public async Task OnGetAsync()
    {
        var query = _context.Motorcycles
            .Include(m => m.Owner)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(SearchPlate))
        {
            var term = SearchPlate.Trim();
            query = query.Where(m => m.PlateNumber.Contains(term));
        }

        Motorcycles = await query
            .OrderBy(m => m.PlateNumber)
            .ToListAsync();
    }
}
