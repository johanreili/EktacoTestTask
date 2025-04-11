using ProductAPI.Common.Utilities;
using Swashbuckle.AspNetCore.Filters;

namespace ProductAPI.Api.Configuration.Swagger.Examples;

/// <summary>
/// Validation error
/// </summary>
public class ValidationErrorExample : IExamplesProvider<Result>
{
    private static readonly string[] ProductNameErrors = { "Product name is required." };
    private static readonly string[] PriceErrors = { "Price must be greater than 0." };

    /// <summary>
    /// Validation error
    /// </summary>
    /// <returns>Validation error</returns>
    public Result GetExamples()
    {
        return Result.ValidationFailure(
            new Dictionary<string, string[]>
            {
                { "ProductName", ProductNameErrors },
                { "Price", PriceErrors }
            }
        );
    }
}
