using Microsoft.EntityFrameworkCore;
using Moto_Taxi_Maintenance_Tracker.Models;

namespace Moto_Taxi_Maintenance_Tracker.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Garage> Garages => Set<Garage>();
    public DbSet<Motorcycle> Motorcycles => Set<Motorcycle>();
    public DbSet<ServiceRequest> ServiceRequests => Set<ServiceRequest>();
    public DbSet<Repair> Repairs => Set<Repair>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Motorcycle>()
            .HasIndex(m => m.PlateNumber)
            .IsUnique();

        modelBuilder.Entity<Motorcycle>()
            .HasOne(m => m.Owner)
            .WithMany(u => u.Motorcycles)
            .HasForeignKey(m => m.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
        // Service Request - Motorcycle
        modelBuilder.Entity<ServiceRequest>()
            .HasOne(s => s.Motorcycle)
            .WithMany()
            .HasForeignKey(s => s.MotorcycleId)
            .OnDelete(DeleteBehavior.Restrict);

        // Service Request - User
        modelBuilder.Entity<ServiceRequest>()
            .HasOne(s => s.User)
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Repair - Service Request
        modelBuilder.Entity<Repair>()
            .HasOne(r => r.ServiceRequest)
            .WithMany()
            .HasForeignKey(r => r.ServiceRequestId)
            .OnDelete(DeleteBehavior.Restrict);

        // Repair - Mechanic
        modelBuilder.Entity<Repair>()
            .HasOne(r => r.Mechanic)
            .WithMany()
            .HasForeignKey(r => r.MechanicId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
