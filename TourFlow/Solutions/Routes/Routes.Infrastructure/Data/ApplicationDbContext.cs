using Microsoft.EntityFrameworkCore;
using Routes.Domain.Entities;
using Routes.Domain.ValueObjects;

namespace Routes.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Route> Routes => Set<Route>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureRoute(modelBuilder);
        }

        private void ConfigureRoute(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Route>(entity =>
            {
                entity.ToTable("Routes");

                entity.HasKey(r => r.RouteId);

                entity.Property(r => r.RouteId)
                    .IsRequired();

                entity.HasIndex(r => r.RouteId)
                    .IsUnique();

                entity.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(r => r.Description)
                    .HasMaxLength(500);

                entity.Property(r => r.BasePrice)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(r => r.CreatedAt)
                    .IsRequired();

                entity.Property(r => r.UpdatedAt);

                entity.OwnsMany(r => r.Locations, b =>
                {
                    b.ToTable("RouteLocations");

                    b.WithOwner()
                     .HasForeignKey("RouteId");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();
                    b.HasKey("Id");

                    b.Property(x => x.Location)
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property(x => x.StayDurationDays)
                        .IsRequired();
                });

                entity.Navigation(r => r.Locations)
                    .UsePropertyAccessMode(PropertyAccessMode.Field);
            });
        }
    }
}
