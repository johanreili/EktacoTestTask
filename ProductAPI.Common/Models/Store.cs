namespace ProductAPI.Common.Models;

public class Store
{
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public ICollection<ProductStore> ProductStores { get; set; }
}
