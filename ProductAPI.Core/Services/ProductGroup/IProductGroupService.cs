using ProductAPI.Common.Dtos.ProductGroup;
using ProductAPI.Common.Utilities;

namespace ProductAPI.Core.Services.ProductGroup;

public interface IProductGroupService
{
    Task<int> GetMaxProductGroupIdAsync();
    Task<Result<List<ProductGroupReturnAsTreeDto>>> GetProductGroupTreeAsync();
}
