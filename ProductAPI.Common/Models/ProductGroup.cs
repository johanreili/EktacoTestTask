namespace ProductAPI.Common.Models;

public class ProductGroup
{
    public int ProductGroupId { get; set; }
    public string ProductGroupName { get; set; }
    public int? ParentGroupId { get; set; }
    public ProductGroup ParentGroup { get; set; }
    public ICollection<ProductGroup> ChildGroups { get; set; }
    public ICollection<Product> Products { get; set; }
}
