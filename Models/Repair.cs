
using System;
using System.ComponentModel.DataAnnotations;

namespace Moto_Taxi_Maintenance_Tracker.Models
{
    public class Repair
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ServiceRequestId { get; set; }

        [Required]
        public int MechanicId { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Assigned";

        public string? RepairNotes { get; set; }

        [Range(0, 100000000)]
        public decimal LabourCost { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CompletedDate { get; set; }
        public ServiceRequest? ServiceRequest { get; set; }

        public User? Mechanic { get; set; }
    }
}
