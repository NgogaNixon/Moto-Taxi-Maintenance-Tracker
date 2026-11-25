using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moto_Taxi_Maintenance_Tracker.Models;

public class Motorcycle
{
    public int Id { get; set; }

    [Required, StringLength(20)]
    [Display(Name = "Plate Number")]
    public string PlateNumber { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string Model { get; set; } = string.Empty;

    [Range(1980, 2100)]
    public int Year { get; set; }

    [Required]
    [Display(Name = "Owner")]
    public int OwnerId { get; set; }

    [ForeignKey(nameof(OwnerId))]
    public User? Owner { get; set; }
}
