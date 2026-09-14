using Product_Service.Exceptions;

namespace Product_Service.Models.Domain;


public class ProductSizes
{
    private List<string> _tallas = [];
    public List<string> Tallas
    {
        get => _tallas;
        set => _tallas = DataValidator(value);
    }

    private List<string> DataValidator(List<string> values)
    {
        List<string> newValues = [];

        foreach (string value in values)
        {
            newValues.Add(SizeValidator(value));
        }

        List<string> tallasPermitidas =
        [
          "XXS",
          "XS",
          "S",
          "M",
          "L",
          "XL",
          "XXL",
          "XXXL"
        ];

        return newValues.OrderBy(value =>
        {
            if (decimal.TryParse(value, out decimal numero))
                return 0;

            return 1;
        }).ThenBy(value =>
        {
            if (decimal.TryParse(value, out decimal numero))
                return numero;

            return tallasPermitidas.IndexOf(value);
        }).ToList();
    }

    private static string SizeValidator(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) { throw new SizeValueException("La talla no puede tener valor nulo o vacio."); }

        value = value.Trim().ToUpper();

        string[] tallasPermitidas =
        [
          "XXS",
          "XS",
          "S",
          "M",
          "L",
          "XL",
          "XXL",
          "XXXL"
        ];

        if (tallasPermitidas.Contains(value)) { return value; }

        value = value.Replace(",", ".");

        if (decimal.TryParse(value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out decimal numero))
        {
            if (numero > 0 && numero < 70)
            {
                return value;
            }
        }

        throw new SizeValueException($"La talla ingresada no es valida, valor ingresado {value}");
    }

}
