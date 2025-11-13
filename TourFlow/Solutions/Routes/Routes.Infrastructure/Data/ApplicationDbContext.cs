using Microsoft.EntityFrameworkCore;
using Routes.Domain.Entities;
using Routes.Domain.ValueObjects;

namespace Routes.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Route> Routes => Set<Route>();
        public DbSet<RouteLocation> RouteLocations => Set<RouteLocation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Route>(entity =>
            {
                entity.ToTable("Routes");

                entity.HasKey(r => r.Id);
                entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
                entity.Property(r => r.Description).HasMaxLength(500);
                entity.Property(r => r.BasePrice).HasColumnType("decimal(18,2)");
                entity.Property(r => r.CreatedAt).IsRequired();
                entity.Property(r => r.UpdatedAt);

                entity.HasMany(r => r.Locations)
                      .WithOne(rl => rl.Route)
                      .HasForeignKey(rl => rl.RouteId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RouteLocation>(entity =>
            {
                entity.ToTable("RouteLocations");

                entity.HasKey(rl => rl.Id);
                entity.Property(rl => rl.Location).IsRequired().HasMaxLength(100);
                entity.Property(rl => rl.StayDurationDays).IsRequired();
            });
        }
    }
}

