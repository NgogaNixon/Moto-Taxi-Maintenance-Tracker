using System.ComponentModel.DataAnnotations;

namespace Moto_Taxi_Maintenance_Tracker.Models;

public class User
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public UserRole Role { get; set; } = UserRole.Owner;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Motorcycle> Motorcycles { get; set; } = new List<Motorcycle>();
}
