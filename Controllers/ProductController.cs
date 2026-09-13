using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Product_Service.Models.Domain;
using Product_Service.Models.DTOs;
using Product_Service.Service;

namespace Product_Service.Controllers;

[ApiController]
[Route("/products")]
public class ProductController : ControllerBase
{
    private readonly ProductService _service;

    public ProductController(ProductService service)
    {
        _service = service;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<IReadOnlyList<ProductResponse>>> GetAllProducts()
    {
        return Ok(await _service.GetAllProductsAsync());
    }

    [HttpGet("{id:int}")]
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

    [HttpGet("{id:int}")]
    [MapToApiVersion("2.0")]
    public async Task<ActionResult> GetProductByIdV2(int id)
    {
        ProductResponse? response = await _service.GetProductByIdAsync(id);
        if (response == null) { return NotFound(); }

        return RedirectToAction(nameof(GetByIdWhitSlug), new { slug = response.Name.GenerateSlugFrom() });
    }

    [HttpGet("{slug}", Name = nameof(GetByIdWhitSlug))]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public async Task<ActionResult<ProductResponse>> GetByIdWhitSlug(string slug)
    {
        ProductResponse? response = await _service.GetProductBySlugAsync(slug);
        if (response == null) { return NotFound(); }

        return Ok(response);


    }

    [HttpGet("by-category/{category}")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<IReadOnlyList<ProductResponse>>> GetProductsByCategory(string category)
    {
        var products = await _service.GetProductsByCategoryAsync(category);

        return products switch
        {
            null => NotFound(),
            _ => Ok(products)
        };
    }

    [HttpGet("by-price/{price:int}")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<IReadOnlyList<ProductResponse>>> GetProductsByPrice(int price)
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

    [HttpPut("{id:int}")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<ProductResponse>> UpdateProduct(int id, ProductRequest request)
    {
        ProductResponse? product = await _service.UpdateProductAsync(id, request);

        return product switch
        {
            null => BadRequest(),
            _ => Ok(product)
        };
    }

    [HttpDelete("{id:int}")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult> DeleteProductById(int id)
    {
        bool isDeleted = await _service.DeleteProductById(id);
        return isDeleted switch
        {
            false => NotFound(),
            true => NoContent()
        };
    }

}
