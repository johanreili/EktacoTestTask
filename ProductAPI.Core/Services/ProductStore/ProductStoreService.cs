using ProductAPI.Common.Utilities;
using ProductAPI.Data.Repositories.ProductStore;

namespace ProductAPI.Core.Services.ProductStore;

public class ProductStoreService(IProductStoreRepository productStoreRepository)
    : IProductStoreService
{
    private readonly IProductStoreRepository _productStoreRepository = productStoreRepository;

    public async Task<Result> AddProductToStoreAsync(int productId, IEnumerable<int> storeIds)
    {
        // Add product to stores if provided
        if (storeIds != null && storeIds.Any())
        {
            foreach (var storeId in storeIds)
            {
                await _productStoreRepository.AddProductToStoreAsync(productId, storeId);
            }

            // Save changes after adding product-store relationships
            await _productStoreRepository.SaveChangesAsync();
        }

        return Result.Success();
    }
}
