using System;
using System.ComponentModel.DataAnnotations;

namespace Moto_Taxi_Maintenance_Tracker.Models
{
    public class ServiceRequest
    {
        [Key]
        public int ServiceRequestId { get; set; }

        [Required]
        public int MotorcycleId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string ServiceType { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime RequestDate { get; set; } = DateTime.Now;

        [Required]
        public string Status { get; set; } = "Pending";

        public string? Priority { get; set; }

        public string? AdminNotes { get; set; }

        public Motorcycle? Motorcycle { get; set; }

        public User? User { get; set; }
    }

}
    
