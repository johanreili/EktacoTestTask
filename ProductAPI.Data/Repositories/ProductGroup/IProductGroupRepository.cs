using ProductAPI.Data.Repositories.Base;
using CommonProductGroup = ProductAPI.Common.Models.ProductGroup;

namespace ProductAPI.Data.Repositories.ProductGroup;

public interface IProductGroupRepository : IRepositoryBase<CommonProductGroup>
{
    Task<int> GetLastProductGroupIdAsync();
}
