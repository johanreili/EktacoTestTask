namespace ProductAPI.Common.Models;

public class ProductStore
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int StoreId { get; set; }
    public Product Product { get; set; }
    public Store Store { get; set; }
}
