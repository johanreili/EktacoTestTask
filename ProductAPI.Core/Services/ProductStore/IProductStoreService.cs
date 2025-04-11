using ProductAPI.Common.Utilities;

namespace ProductAPI.Core.Services.ProductStore;

public interface IProductStoreService
{
    Task<Result> AddProductToStoreAsync(int productId, IEnumerable<int> storeIds);
}
