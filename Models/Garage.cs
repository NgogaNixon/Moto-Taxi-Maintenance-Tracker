using System.ComponentModel.DataAnnotations;

namespace Moto_Taxi_Maintenance_Tracker.Models;

public class Garage
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string Location { get; set; } = string.Empty;
}
