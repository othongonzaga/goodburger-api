using goodburger_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace goodburger_api.Infrastructure.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders");

            entity.HasKey(order => order.Id);

            entity.Property(order => order.Subtotal)
                .HasPrecision(10, 2);

            entity.Property(order => order.Discount)
                .HasPrecision(10, 2);

            entity.Property(order => order.Total)
                .HasPrecision(10, 2);
        });
    }
}