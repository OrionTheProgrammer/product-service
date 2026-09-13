using Product_Service.Models.Domain;
using Product_Service.Models.DTOs;

namespace Product_Service.Models.Mappers;


public static class RequestToModel
{
    public static Product ToModel(this ProductRequest request)
    {
        return new Product
        {
            ProductName = request.Name,
            ProductBrand = request.Brand,
            ProductCategory = request.Category,
            ProductPrice = request.Price,
            ProductSizes = request.Sizes
        };
    }
}
