namespace ProductAPI.Common.Models;

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public ProductGroup ProductGroup { get; set; }
    public int ProductGroupId { get; set; }
    public DateTime DateAdded { get; set; }
    public decimal SalePrice { get; set; }
    public decimal SalePriceWithVAT { get; set; }
    public decimal VATRate { get; set; }
    public ICollection<ProductStore> ProductStores { get; set; }
}
