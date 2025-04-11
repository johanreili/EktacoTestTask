namespace ProductAPI.Data.Repositories.Store;

public class StoreService(IStoreRepository storeRepository) : IStoreService
{
    private readonly IStoreRepository _storeRepository = storeRepository;

    public async Task<bool> DoStoresExistAsync(IEnumerable<int> storeIds)
    {
        var existingStoreIds = await _storeRepository.GetExistingStoreIdsAsync(storeIds);
        return storeIds.All(id => existingStoreIds.Contains(id));
    }
}
