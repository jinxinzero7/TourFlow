using Microsoft.EntityFrameworkCore;
using Routes.API.Domain.Entities;
using Routes.API.Domain.ValueObjects;

namespace Routes.API.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Domain.Entities.Route> Routes => Set<Domain.Entities.Route>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // конфигурация сущности
            modelBuilder.Entity<Domain.Entities.Route>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(r => r.Description)
                    .HasMaxLength(1000);

                entity.Property(r => r.BasePrice)
                    .HasPrecision(18, 2);

                entity.Property(r => r.DurationDays)
                    .IsRequired();

                entity.Property(r => r.CreatedAt)
                    .IsRequired();

                entity.Property(r => r.IsActive)
                    .IsRequired();

                // конфиг для owned type value object
                entity.OwnsMany(r => r.Locations, location =>
                {
                    location.WithOwner().HasForeignKey("RouteId");
                    location.Property<string>("Id").ValueGeneratedOnAdd();
                    location.HasKey("Id");

                    location.Property(l => l.Location)
                        .IsRequired()
                        .HasMaxLength(100);

                    location.Property(l => l.StayDurationDays)
                        .IsRequired();
                });
            });
        }
    }
}

