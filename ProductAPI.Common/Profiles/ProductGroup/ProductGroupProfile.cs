using AutoMapper;
using ProductAPI.Common.Dtos.ProductGroup;
using CommonProductGroup = ProductAPI.Common.Models.ProductGroup;

namespace ProductAPI.Common.Profiles.ProductGroup;

public class ProductGroupProfile : Profile
{
    public ProductGroupProfile()
    {
        CreateMap<CommonProductGroup, ProductGroupReturnAsTreeDto>()
            .ForMember(
                dest => dest.ChildGroups,
                opt => opt.MapFrom(src => new List<ProductGroupReturnAsTreeDto>())
            );
    }
}
