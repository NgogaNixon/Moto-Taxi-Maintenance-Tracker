namespace Moto_Taxi_Maintenance_Tracker.Models
{
    public class Repair
    {
        public int RepairId { get; set; }
        public int RequestId { get; set; }
        public ServiceRequest? ServiceRequest { get; set; }
        public int? MechanicId { get; set; }
        public User? Mechanic { get; set; }
        public int GarageId { get; set; }
        public Garage? Garage { get; set; }
        public decimal LabourCost { get; set; }
        public string Notes { get; set; } = "";
        public DateTime DateStarted { get; set; } = DateTime.Now;
        public DateTime? DateCompleted { get; set; }
    }
}