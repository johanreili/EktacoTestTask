using ProductAPI.Common.Dtos.Product;
using ProductAPI.Common.Utilities;

namespace ProductAPI.Core.Services.Product;

public interface IProductService
{
    Task<Result> AddProductAsync(ProductCreateDto productDto);

    Task<Result<ProductReturnDto>> GetProductByIdAsync(int productId);
    Task<Result<IEnumerable<ProductReturnDto>>> GetAllProductsAsync();
}
