using Product_Service.Models.DTOs;
using Product_Service.Models.Entities;

namespace Product_Service.Models.Mappers;


public static class EntityToResponse
{
    public static ProductResponse ToResponse(this ProductEntity product)
    {
        return new ProductResponse
        {
            Id = product.ProductId,
            Name = product.ProductName,
            Brand = product.ProductBrand,
            Category = product.ProductCategory.GetStringValue(),
            Price = product.ProductPrice,
            Sizes = product.ProductSizes
        };
    }
}
