using AutoMapper;
using ProductAPI.Common.Dtos.ProductGroup;
using ProductAPI.Data.Repositories.ProductGroup;
using ProductAPI.Common.Utilities;

namespace ProductAPI.Core.Services.ProductGroup;

public class ProductGroupService(IProductGroupRepository productGroupRepository, IMapper mapper)
    : IProductGroupService
{
    private readonly IProductGroupRepository _productGroupRepository = productGroupRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<int> GetMaxProductGroupIdAsync()
    {
        return await _productGroupRepository.GetLastProductGroupIdAsync();
    }

    public async Task<Result<List<ProductGroupReturnAsTreeDto>>> GetProductGroupTreeAsync()
    {
        try
        {
            var productGroups = await _productGroupRepository.GetAllAsync();

            // Dictionary for quick lookup
            var groupDictionary = productGroups.ToDictionary(
                pg => pg.ProductGroupId,
                _mapper.Map<ProductGroupReturnAsTreeDto>
            );

            // Create the tree structure
            var rootGroups = new List<ProductGroupReturnAsTreeDto>();

            foreach (var group in productGroups)
            {
                var mappedGroup = groupDictionary[group.ProductGroupId];

                if (group.ParentGroupId == null)
                {
                    // Root group
                    rootGroups.Add(mappedGroup);
                }
                else if (
                    groupDictionary.TryGetValue(group.ParentGroupId.Value, out var parentGroup)
                )
                {
                    // Add to parent's child list
                    parentGroup.ChildGroups.Add(mappedGroup);
                }
            }

            // Return success result with the tree structure
            return Result<List<ProductGroupReturnAsTreeDto>>.Success(rootGroups);
        }
        catch (Exception ex)
        {
            // Return failure result with the error message
            return Result<List<ProductGroupReturnAsTreeDto>>.Failure(
                $"An error occurred while fetching the product group tree: {ex.Message}"
            );
        }
    }
}
