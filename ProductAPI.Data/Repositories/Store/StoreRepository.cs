using Microsoft.EntityFrameworkCore;
using ProductAPI.Data.Data;
using ProductAPI.Data.Repositories.Base;
using CommonStore = ProductAPI.Common.Models.Store;

namespace ProductAPI.Data.Repositories.Store;

public class StoreRepository(ApplicationDbContext context)
    : RepositoryBase<CommonStore>(context),
        IStoreRepository
{
    public async Task<IEnumerable<int>> GetExistingStoreIdsAsync(IEnumerable<int> storeIds)
    {
        return await _context.Stores
            .Where(store => storeIds.Contains(store.StoreId))
            .Select(store => store.StoreId)
            .ToListAsync();
    }
}
