
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moto_Taxi_Maintenance_Tracker.Data;
using Moto_Taxi_Maintenance_Tracker.Models;

namespace Moto_Taxi_Maintenance_Tracker.Pages.Requests
{
    public class AssignModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AssignModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Repair Repair { get; set; } = new();

        public SelectList Mechanics { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var request = await _context.ServiceRequests
                .FindAsync(id);

            if (request == null)
                return NotFound();

            Repair.ServiceRequestId = id;
            await LoadMechanicsAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Repair.ServiceRequestId = id;
            Repair.Status = "Assigned";

            var requestExists = await _context.ServiceRequests
                .AnyAsync(s => s.ServiceRequestId == id);

            var mechanicExists = await _context.Users
                .AnyAsync(u => u.Id == Repair.MechanicId
                    && u.Role == UserRole.Mechanic);

            if (!requestExists || !mechanicExists)
            {
                ModelState.AddModelError(string.Empty,
                    "Select a valid request and mechanic.");
            }

            if (!ModelState.IsValid)
            {
                await LoadMechanicsAsync();
                return Page();
            }

            _context.Repairs.Add(Repair);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }

        private async Task LoadMechanicsAsync()
        {
            var mechanics = await _context.Users
                .Where(u => u.Role == UserRole.Mechanic)
                .ToListAsync();

            Mechanics = new SelectList(
                mechanics, "Id", "FullName");
        }
    }
}
