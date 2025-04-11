using AutoMapper;
using ProductAPI.Common.Dtos.Product;
using CommonProduct = ProductAPI.Common.Models.Product;

namespace ProductAPI.Common.Profiles.Product;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductCreateDto, CommonProduct>();
        CreateMap<CommonProduct, ProductReturnDto>()
            .ForMember(
                dest => dest.ProductGroupName,
                opt => opt.MapFrom(src => src.ProductGroup.ProductGroupName)
            )
            .ForMember(
                dest => dest.Stores,
                opt =>
                    opt.MapFrom(src => src.ProductStores.Select(ps => ps.Store.StoreName).ToList())
            );
        CreateMap<ProductReturnDto, ProductIdGetDto>();
        CreateMap<ProductIdGetDto, ProductReturnDto>();
    }
}
