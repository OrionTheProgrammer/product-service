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
        product.ComplexProperty(p => p.ProductCategory, category =>
        {
            category.Property(c => c.Type).HasConversion<string>();
            category.Property(c => c.OriginalValue);
        });
        product.Property(p => p.ProductPrice).IsRequired();
        product.OwnsOne(p => p.ProductSizes, sizes =>
        {
            sizes.Property(s => s.XS)
            .HasColumnName("XS");

            sizes.Property(s => s.S)
            .HasColumnName("S");

            sizes.Property(s => s.M)
            .HasColumnName("M");

            sizes.Property(s => s.L)
            .HasColumnName("L");

            sizes.Property(s => s.XL)
            .HasColumnName("XL");

            sizes.Property(s => s.XXL)
            .HasColumnName("XXL");
        });
    }
}
