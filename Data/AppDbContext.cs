using Microsoft.EntityFrameworkCore;
using Moto_Taxi_Maintenance_Tracker.Models;

namespace Moto_Taxi_Maintenance_Tracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Garage> Garages { get; set; }
        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<SparePart> SpareParts { get; set; }
        public DbSet<RepairPart> RepairParts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Motorcycle>().HasKey(m => m.MotoId);
            modelBuilder.Entity<ServiceRequest>().HasKey(s => s.RequestId);
            modelBuilder.Entity<SparePart>().HasKey(p => p.PartId);

            modelBuilder.Entity<Repair>().Property(r => r.LabourCost).HasPrecision(18, 2);
            modelBuilder.Entity<SparePart>().Property(p => p.UnitPrice).HasPrecision(18, 2);
        }
    }
}