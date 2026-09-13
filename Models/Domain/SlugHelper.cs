using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Product_Service.Models.Domain;


public static class SlugHelper
{
    public static string GenerateSlug(string productName)
    {
        productName = productName.ToLowerInvariant().Normalize(NormalizationForm.FormD);

        var builder = new StringBuilder();

        foreach (var c in productName)
        {
            var category = CharUnicodeInfo.GetUnicodeCategory(c);

            if (category == UnicodeCategory.NonSpacingMark)
            {
                builder.Append(c);
            }
        }

        var productSlug = builder.ToString().Normalize(NormalizationForm.FormC);

        productSlug = Regex.Replace(productSlug, @"[^a-z0-9\s-]", "");
        productSlug = Regex.Replace(productSlug, @"[\s-]+", "-");

        return productSlug.Trim();
    }

    public static string GenerateSlugFrom(this string productName)
    {
        productName = productName.ToLowerInvariant().Normalize(NormalizationForm.FormD);

        var builder = new StringBuilder();

        foreach (var c in productName)
        {
            var category = CharUnicodeInfo.GetUnicodeCategory(c);

            if (category != UnicodeCategory.NonSpacingMark)
            {
                builder.Append(c);
            }
        }

        var productSlug = builder.ToString().Normalize(NormalizationForm.FormC);

        productSlug = Regex.Replace(productSlug, @"[^a-z0-9\s-]", "");
        productSlug = Regex.Replace(productSlug, @"[\s-]+", "-");

        return productSlug.Trim();
    }
}
