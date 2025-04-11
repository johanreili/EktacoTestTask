using ProductAPI.Data.Repositories.Base;
using CommonStore = ProductAPI.Common.Models.Store;

namespace ProductAPI.Data.Repositories.Store;

public interface IStoreRepository : IRepositoryBase<CommonStore>
{
    Task<IEnumerable<int>> GetExistingStoreIdsAsync(IEnumerable<int> storeIds);
}
