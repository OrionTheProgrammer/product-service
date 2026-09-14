using Product_Service.Exceptions;
using Product_Service.Models.Domain;
using Product_Service.Models.DTOs;
using Product_Service.Models.Entities;
using Product_Service.Models.Mappers;
using Product_Service.Repository;

namespace Product_Service.Service;


public class ProductService
{
    private readonly IProductRepository _repository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository repository, ILogger<ProductService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IReadOnlyList<ProductResponse>> GetAllProductsAsync()
    {
        var products = await _repository.GetAllProductsAsync();
        return products.Select(p => p.ToResponse()).ToList();
    }

    public async Task<ProductResponse> GetProductByIdAsync(int id)
    {
        ProductEntity? product = await _repository.GetProductByIdAsync(id) ?? throw new ProductNotFoundException(id);
        return product.ToResponse();
    }

    public async Task<ProductResponse> GetProductBySlugAsync(string slug)
    {
        ProductEntity? product = await _repository.GetProductBySlugAsync(slug) ?? throw new ProductNotFoundException($"No se encontro ningun producto relacionado a {slug}");
        return product.ToResponse();
    }

    public async Task<ProductResponse> CreateProductAsync(ProductRequest request)
    {
        Product productModel = new(
            request.Name, request.Brand, request.Category, request.Price, request.Sizes
        );

        _logger.LogInformation("Creando producto, nombre base: {ProductName}", request.Name);

        ProductEntity productEntity = new()
        {
            ProductName = productModel.ProductName,
            ProductBrand = productModel.ProductBrand,
            ProductCategory = Category.From(productModel.ProductCategory),
            ProductPrice = productModel.ProductPrice,
            ProductSizes = productModel.ProductSizes,
            ProductSlug = productModel.ProductName.GenerateSlugFrom()
        };

        ProductEntity saved = await _repository.AddProductAsync(productEntity);
        return saved.ToResponse();
    }

    public async Task<ProductResponse> UpdateProductAsync(int id, ProductRequest product)
    {
        ProductEntity? newProduct = await _repository.UpdateProductAsync(id, product) ?? throw new ProductNotFoundException($"No se pudo actualizar el producto, no existe con el ID {id}");
        return newProduct.ToResponse();
    }

    public async Task<bool> DeleteProductById(int id)
    {
        return await _repository.DeleteProductByIdAsync(id);
    }

    public async Task<IReadOnlyList<ProductResponse>> GetProductsByCategoryAsync(string category)
    {
        var products = await _repository.GetProductsByCategoryAsync(category);
        if (products == null) { throw new CategoryValueException($"Categoria ingresada no valida. {category}"); }

        return products.Select(p => p.ToResponse()).ToList();
    }

    public async Task<IReadOnlyList<ProductResponse>> GetProductsByPriceAsync(int price)
    {
        var products = await _repository.GetProductsByPriceAsync(price);

        return products.Select(p => p.ToResponse()).ToList();
    }

    public async Task<IReadOnlyList<ProductResponse>> GetProductsBySizeAsync(string size)
    {
        var products = await _repository.GetProductsBySizeAsync(size);

        return products.Select(p => p.ToResponse()).ToList();
    }
}
