using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Product_Service.Models.Domain;
using Product_Service.Models.DTOs;
using Product_Service.Models.Entities;
using Product_Service.Service;

namespace Product_Service.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly ProductService _service;

    public ProductController(ProductService service)
    {
        _service = service;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<ProductResponse>> GetAllProducts()
    {
        return Ok(await _service.GetAllProductsAsync());
    }

    [HttpGet("/{id:int}")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<ProductResponse>> GetProductById(int id)
    {
        ProductResponse? response = await _service.GetProductByIdAsync(id);
        return response switch
        {
            null => NotFound(),
            _ => Ok(response)
        };
    }

    [HttpGet("/{category:string}")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<ProductResponse>> GetProductsByCategory(string category)
    {
        var products = await _service.GetProductsByCategoryAsync(category);

        return products switch
        {
            null => NotFound(),
            _ => Ok(products)
        };
    }

    [HttpGet("/{price:int}")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<ProductResponse>> GetProductsByPrice(int price)
    {
        var products = await _service.GetProductsByPriceAsync(price);

        return products switch
        {
            null => NotFound(),
            _ => Ok(products)
        };
    }

    [HttpPost]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<ProductResponse>> CreateProduct(ProductRequest request)
    {
        ProductResponse product = await _service.CreateProductAsync(request);
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    [HttpPut]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult> UpdateProduct(ProductEntity product)
    {
        bool isSusses = await _service.UpdateProductAsync(product);
        return isSusses switch
        {
            false => NotFound(),
            true => Ok(isSusses)
        };
    }

    [HttpDelete("/{id:int}")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult> DeleteProductById(int id)
    {
        bool isDeleted = await _service.DeleteProductById(id);
        return isDeleted switch
        {
            false => NotFound(),
            true => Ok(isDeleted)
        };
    }

}
