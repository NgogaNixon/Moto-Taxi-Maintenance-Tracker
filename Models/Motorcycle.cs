namespace Moto_Taxi_Maintenance_Tracker.Models
{
    public class Motorcycle
    {
        public int MotoId { get; set; }
        public string PlateNumber { get; set; } = "";
        public string Model { get; set; } = "";
        public int Year { get; set; }
        public int OwnerId { get; set; }
        public User? Owner { get; set; }
    }
}