using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moto_Taxi_Maintenance_Tracker.Data;
using Moto_Taxi_Maintenance_Tracker.Models;

namespace Moto_Taxi_Maintenance_Tracker.Pages.Requests
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel (ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ServiceRequest ServiceRequest { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            ServiceRequest.UserId = 1; // Temporary placeholder
            ServiceRequest.RequestDate = DateTime.Now;
            ServiceRequest.Status = "Pending";

            _context.ServiceRequests.Add(ServiceRequest);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}