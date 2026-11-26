namespace Moto_Taxi_Maintenance_Tracker.Models
{
    public class ServiceRequest
    {
        public int RequestId { get; set; }
        public int MotoId { get; set; }
        public Motorcycle? Motorcycle { get; set; }
        public string Description { get; set; } = "";
        public DateTime DateRequested { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Requested";
    }
}