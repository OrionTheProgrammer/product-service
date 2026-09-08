using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Product_Service.Data;
using Product_Service.Exceptions;
using Product_Service.Models.Domain;
using Product_Service.Models.Entities;

namespace Product_Service.Repository;


public class SqliteProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public SqliteProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<ProductEntity>> GetAllProductsAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .OrderBy(p => p.ProductId)
            .ToListAsync();
    }

    public async Task<ProductEntity?> GetProductByIdAsync(int id)
    {
        return await _context.Products
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.ProductId == id);
    }

    public async Task<ProductEntity> AddProductAsync(ProductEntity product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task UpdateProductAsync(ProductEntity product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteProductByIdAsync(int id)
    {
        ProductEntity? product = await _context.Products.SingleOrDefaultAsync(p => p.ProductId == id);

        if (product == null)
        {
            return false;
        }

        return true;
    }

    public async Task<IReadOnlyList<ProductEntity>> GetProductsByCategoryAsync(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
        {
            throw new ArgumentException("La categoria escrita no puede ser null o estar vacia.");
        }

        return await _context.Products
            .AsNoTracking()
            .Where(p => p.ProductCategory == Category.From(category))
            .OrderBy(p => p.ProductId)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<ProductEntity>> GetProductsByPriceAsync(int price)
    {
        if (price <= 0)
        {
            throw new PriceValueException("El precio no puede ser cero ni negativo.");
        }

        return await _context.Products
            .AsNoTracking()
            .Where(p => p.ProductPrice == price)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<ProductEntity>> GetProductsBySizeAsync(string size)
    {
        if (string.IsNullOrWhiteSpace(size))
        {
            throw new ArgumentException("La talla de filtrado no puede ser null ni estar vacia.");
        }

        if (Enum.TryParse<Size>(size, true, out var parsedSize))
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => p.ProductSizes.Sizes.GetValueOrDefault(parsedSize) == true)
                .ToListAsync();
        }

        throw new ArgumentException("Error al parsear la categoria.");

    }
}
