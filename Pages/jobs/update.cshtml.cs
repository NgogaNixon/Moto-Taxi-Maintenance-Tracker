using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Moto_Taxi_Maintenance_Tracker.Data;
using Moto_Taxi_Maintenance_Tracker.Models;

namespace Moto_Taxi_Maintenance_Tracker.Pages.Jobs
{
    public class UpdateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public UpdateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Repair Repair { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var repair = await _context.Repairs.FindAsync(id);

            if (repair == null)
                return NotFound();

            Repair = repair;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var repair = await _context.Repairs.FindAsync(id);

            if (repair == null)
                return NotFound();

            if (Repair.Status != "Assigned" &&
                Repair.Status != "In Progress" &&
                Repair.Status != "Completed")
            {
                ModelState.AddModelError(
                    "Repair.Status", "Select a valid status.");
            }

            if (Repair.LabourCost < 0)
            {
                ModelState.AddModelError(
                    "Repair.LabourCost", "Labour cost cannot be negative.");
            }

            if (!ModelState.IsValid)
                return Page();

            repair.Status = Repair.Status;
            repair.RepairNotes = Repair.RepairNotes;
            repair.LabourCost = Repair.LabourCost;

            if (repair.Status == "In Progress" &&
                repair.StartDate == null)
            {
                repair.StartDate = DateTime.Now;
            }

            if (repair.Status == "Completed")
            {
                repair.CompletedDate = DateTime.Now;
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}