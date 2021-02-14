using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Contexts
{
    public class DataContext : DbContext
    {
        private string GetConnectionString()
        {
            var host = System.Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
            var port = System.Environment.GetEnvironmentVariable("DB_PORT") ?? "1433";
            var database = System.Environment.GetEnvironmentVariable("DB_DATABASE") ?? "localiza_api";
            var username = System.Environment.GetEnvironmentVariable("DB_USERNAME") ?? "SA";
            var password = System.Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "yourStrong(!)Password";
            return $"Server={host},{port}; Database={database}; User Id={username}; Password={password}";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }

        private void SetTimestamps()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
            }
        }

        public override int SaveChanges()
        {
            SetTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = new CancellationToken()
        )
        {
            SetTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleBrand> VehiclesBrands { get; set; }
        public DbSet<VehicleCategory> VehiclesCategorys { get; set; }
        public DbSet<VehicleModel> VehiclesModels { get; set; }
        public DbSet<Checklist> Checklists { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
    }
}
