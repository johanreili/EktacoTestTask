namespace ProductAPI.Common.Dtos.Product;

public class ProductReturnDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductGroupName { get; set; }
    public DateTime DateAdded { get; set; }
    public decimal SalePrice { get; set; }
    public decimal SalePriceWithVAT { get; set; }
    public decimal VATRate { get; set; }
    public List<string> Stores { get; set; }
}
