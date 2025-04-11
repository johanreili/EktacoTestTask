using ProductAPI.Data.Data;
using ProductAPI.Data.Repositories.Base;
using CommonProductStore = ProductAPI.Common.Models.ProductStore;

namespace ProductAPI.Data.Repositories.ProductStore;

public class ProductStoreRepository(ApplicationDbContext context)
    : RepositoryBase<CommonProductStore>(context),
        IProductStoreRepository
{
    public async Task AddProductToStoreAsync(int productId, int storeId)
    {
        var productStore = new CommonProductStore { ProductId = productId, StoreId = storeId };

        await AddAsync(productStore);
    }
}
