using ProductAPI.Common.Utilities;
using Swashbuckle.AspNetCore.Filters;

namespace ProductAPI.Api.Configuration.Swagger.Examples;

/// <summary>
/// Product created
/// </summary>
public class ProductCreatedExample : IExamplesProvider<Result>
{
    /// <summary>
    /// Product created
    /// </summary>
    /// <returns>Product successfully created</returns>
    public Result GetExamples()
    {
        return Result.Success();
    }
}
