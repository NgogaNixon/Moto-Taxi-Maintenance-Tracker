namespace Moto_Taxi_Maintenance_Tracker.Models
{
    public class SparePart
    {
        public int PartId { get; set; }
        public string Name { get; set; } = "";
        public decimal UnitPrice { get; set; }
        public int QuantityInStock { get; set; }
    }
}