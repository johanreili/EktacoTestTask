using ProductAPI.Common.Dtos.ProductGroup;
using ProductAPI.Common.Utilities;
using Swashbuckle.AspNetCore.Filters;

namespace ProductAPI.Api.Configuration.Swagger.Examples;

/// <summary>
/// Product Group tree example
/// </summary>
public class ProductGroupTreeExample
    : IExamplesProvider<Result<IEnumerable<ProductGroupReturnAsTreeDto>>>
{
    /// <summary>
    /// Get product groups
    /// </summary>
    /// <returns>Product groups in a tree structure</returns>
    public Result<IEnumerable<ProductGroupReturnAsTreeDto>> GetExamples()
    {
        return Result<IEnumerable<ProductGroupReturnAsTreeDto>>.Success(
            new List<ProductGroupReturnAsTreeDto>
            {
                new()
                {
                    ProductGroupId = 1,
                    ProductGroupName = "Main Group",
                    ParentGroupId = 0,
                    ChildGroups = new List<ProductGroupReturnAsTreeDto>
                    {
                        new()
                        {
                            ProductGroupId = 2,
                            ProductGroupName = "Sub Group 1",
                            ParentGroupId = 1
                        },
                        new()
                        {
                            ProductGroupId = 3,
                            ProductGroupName = "Sub Group 2",
                            ParentGroupId = 1
                        }
                    }
                }
            }
        );
    }
}
