using ProductAPI.Common.Utilities;
using Swashbuckle.AspNetCore.Filters;

namespace ProductAPI.Api.Configuration.Swagger.Examples;

/// <summary>
/// Not found
/// </summary>
public class NotFoundErrorExample : IExamplesProvider<Result>
{
    /// <summary>
    /// Not found
    /// </summary>
    /// <returns>Not found error</returns>
    public Result GetExamples()
    {
        return ResultExtensions.NotFound("Product not found.");
    }
}
