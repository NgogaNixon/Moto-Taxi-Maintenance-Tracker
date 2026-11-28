using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Moto_Taxi_Maintenance_Tracker.Data;
using Moto_Taxi_Maintenance_Tracker.Models;

namespace Moto_Taxi_Maintenance_Tracker.Pages.Maintenance
{
    public class HistoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public HistoryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Repair> Repairs { get; set; } = new List<Repair>();

        public async Task<IActionResult> OnGetAsync(int motorcycleId)
        {
            Repairs = await _context.Repairs
                .Include(r => r.ServiceRequest)
                .ThenInclude(s => s!.Motorcycle)
                .Where(r => r.ServiceRequest != null &&
                            r.ServiceRequest.MotorcycleId == motorcycleId &&
                            r.Status == "Completed")
                .OrderByDescending(r => r.CompletedDate)
                .ToListAsync();

            return Page();
        }
    }
}