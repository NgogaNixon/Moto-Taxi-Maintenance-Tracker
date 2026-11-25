using System.ComponentModel.DataAnnotations;

namespace Moto_Taxi_Maintenance_Tracker.Models;

// Minimal scaffold for the "Users" table (see MotoCare project summary, section 7).
// The `auth` branch (Member 1) owns Register/Login/role-check logic; this entity lives
// here because the Motorcycles branch needs a User to own a Motorcycle, and the Admin
// "manage users" page below needs something to list/edit. Extend freely once auth lands.
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
