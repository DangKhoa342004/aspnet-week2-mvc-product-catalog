using Microsoft.EntityFrameworkCore;
using AspNetWeek2.Mvc.Models;

namespace AspNetWeek2.Mvc.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Product>().HasIndex(p => p.SKU).IsUnique().IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Product>().Property(p => p.RowVersion).IsRowVersion();
        modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);
    }
}