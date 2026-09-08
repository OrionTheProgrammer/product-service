using Microsoft.EntityFrameworkCore;
using Product_Service.Models.Entities;

namespace Product_Service.Data;


public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

    public DbSet<ProductEntity> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var product = modelBuilder.Entity<ProductEntity>();

        product.HasKey(p => p.ProductId);
        product.Property(p => p.ProductName).IsRequired().HasMaxLength(120);
        product.Property(p => p.ProductBrand).IsRequired().HasMaxLength(120);
        product.Property(p => p.ProductCategory).IsRequired();
        product.Property(p => p.ProductPrice).IsRequired();
        product.Property(p => p.ProductSizes).IsRequired();
    }
}
