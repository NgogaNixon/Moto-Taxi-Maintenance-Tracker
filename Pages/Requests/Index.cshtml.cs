using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Moto_Taxi_Maintenance_Tracker.Data;
using Moto_Taxi_Maintenance_Tracker.Models;

namespace Moto_Taxi_Maintenance_Tracker.Pages.Requests
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ServiceRequest> Requests { get; set; }
            = new List<ServiceRequest>();

        public async Task OnGetAsync()
        {
            Requests = await _context.ServiceRequests
                .Include(r => r.Motorcycle)
                .Include(r => r.User)
                .OrderByDescending(r => r.RequestDate)
                .ToListAsync();
        }
    }
}