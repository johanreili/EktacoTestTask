namespace ProductAPI.Common.Dtos.Product;

public class ProductCreateDto
{
    public string ProductName { get; set; }
    public decimal? SalePrice { get; set; }
    public decimal? SalePriceWithVAT { get; set; }
    public decimal? VATRate { get; set; }
    public int ProductGroupId { get; set; }
    public List<int> StoreIds { get; set; }
}
