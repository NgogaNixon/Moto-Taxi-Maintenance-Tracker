using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Moto_Taxi_Maintenance_Tracker.Data;
using Moto_Taxi_Maintenance_Tracker.Models;

namespace Moto_Taxi_Maintenance_Tracker.Pages.Jobs
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Repair> Jobs { get; set; } = new List<Repair>();

        public async Task OnGetAsync()
        {
            Jobs = await _context.Repairs
                .Include(r => r.ServiceRequest)
                .Include(r => r.Mechanic)
                .OrderByDescending(r => r.Id)
                .ToListAsync();
        }
    }
}