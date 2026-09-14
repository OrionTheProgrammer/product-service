using Product_Service.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Product_Service.Models.DTOs;


public class ProductRequest
{
    [Required(
        ErrorMessage = "El nombre del producto es obligatorio."
    )]
    [StringLength(
        120,
        MinimumLength = 5,
        ErrorMessage = "El nombre del producto debe contener entre 5 y 120 caracteres."
    )]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "La marca es obligatoria.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "La marca debe contener entre 3 y 100 caracteres.")]
    public string Brand { get; set; } = null!;

    [Required(ErrorMessage = "La categoria es obligatoria.")]
    public string Category { get; set; } = null!;

    [Range(1, int.MaxValue, ErrorMessage = "El precio debe ser mayor a cero.")]
    public int Price { get; set; }

    [Required(ErrorMessage = "La/Las tallas son necesarias.")]
    public ProductSizes Sizes { get; set; } = null!;

}
