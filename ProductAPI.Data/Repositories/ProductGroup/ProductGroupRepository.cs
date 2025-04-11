using Microsoft.EntityFrameworkCore;
using ProductAPI.Data.Data;
using ProductAPI.Data.Repositories.Base;
using CommonProductGroup = ProductAPI.Common.Models.ProductGroup;

namespace ProductAPI.Data.Repositories.ProductGroup;

public class ProductGroupRepository(ApplicationDbContext context)
    : RepositoryBase<CommonProductGroup>(context),
        IProductGroupRepository
{
    public async Task<int> GetLastProductGroupIdAsync()
    {
        var lastProductGroup = await _dbSet
            .OrderByDescending(pg => pg.ProductGroupId)
            .FirstOrDefaultAsync();
        return lastProductGroup?.ProductGroupId ?? 0; // Return 0 if no ProductGroups exist
    }
}
