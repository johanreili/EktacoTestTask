using ProductAPI.Common.Dtos.Product;
using ProductAPI.Common.Utilities;
using Swashbuckle.AspNetCore.Filters;

namespace ProductAPI.Api.Configuration.Swagger.Examples;

/// <summary>
/// Product list
/// </summary>
public class ProductListExample : IExamplesProvider<Result<IEnumerable<ProductReturnDto>>>
{
    /// <summary>
    ///Product list
    /// </summary>
    /// <returns>Product list</returns>
    public Result<IEnumerable<ProductReturnDto>> GetExamples()
    {
        return Result<IEnumerable<ProductReturnDto>>.Success(
            new List<ProductReturnDto>
            {
                new ProductReturnDto
                {
                    ProductId = 1,
                    ProductName = "Product A",
                    SalePrice = 100,
                    SalePriceWithVAT = 120,
                    VATRate = 20,
                    DateAdded = DateTime.UtcNow,
                    ProductGroupName = "Group A",
                    Stores = new List<string> { "Store 1", "Store 2" }
                },
                new ProductReturnDto
                {
                    ProductId = 2,
                    ProductName = "Product B",
                    SalePrice = 200,
                    SalePriceWithVAT = 240,
                    VATRate = 20,
                    DateAdded = DateTime.UtcNow,
                    ProductGroupName = "Group B",
                    Stores = new List<string> { "Store 3" }
                }
            }
        );
    }
}
