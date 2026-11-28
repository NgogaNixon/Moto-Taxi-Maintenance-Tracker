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
    }
}
