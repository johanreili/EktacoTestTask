namespace ProductAPI.Data.Repositories.Store;

public interface IStoreService
{
    Task<bool> DoStoresExistAsync(IEnumerable<int> storeIds);
}
