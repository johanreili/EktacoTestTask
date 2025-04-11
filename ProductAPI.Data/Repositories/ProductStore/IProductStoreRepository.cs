using ProductAPI.Data.Repositories.Base;
using CommonProductStore = ProductAPI.Common.Models.ProductStore;

namespace ProductAPI.Data.Repositories.ProductStore;

public interface IProductStoreRepository : IRepositoryBase<CommonProductStore>
{
    Task AddProductToStoreAsync(int productId, int storeId);
}
