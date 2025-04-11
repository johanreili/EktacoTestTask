namespace ProductAPI.Common.Dtos.ProductGroup;

public class ProductGroupReturnAsTreeDto
{
    public int ProductGroupId { get; set; }
    public string ProductGroupName { get; set; }
    public int ParentGroupId { get; set; }

    public ICollection<ProductGroupReturnAsTreeDto> ChildGroups { get; set; }
}
