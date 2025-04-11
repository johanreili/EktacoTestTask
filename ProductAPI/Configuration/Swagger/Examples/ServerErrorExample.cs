using ProductAPI.Common.Utilities;
using Swashbuckle.AspNetCore.Filters;

namespace ProductAPI.Api.Configuration.Swagger.Examples;

/// <summary>
/// Server error
/// </summary>
public class ServerErrorExample : IExamplesProvider<Result>
{
    /// <summary>
    /// Server error
    /// </summary>
    /// <returns>Server error</returns>
    public Result GetExamples()
    {
        return Result.Failure("An unexpected error occurred.", "Internal Server Error");
    }
}
