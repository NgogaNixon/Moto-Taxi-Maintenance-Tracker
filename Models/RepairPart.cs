namespace Moto_Taxi_Maintenance_Tracker.Models
{
    public class RepairPart
    {
        public int RepairPartId { get; set; }
        public int RepairId { get; set; }
        public Repair? Repair { get; set; }
        public int PartId { get; set; }
        public SparePart? SparePart { get; set; }
        public int Quantity { get; set; }
    }
}