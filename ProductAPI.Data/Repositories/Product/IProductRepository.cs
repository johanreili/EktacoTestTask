using ProductAPI.Common.Dtos.Product;
using ProductAPI.Data.Repositories.Base;
using CommonProduct = ProductAPI.Common.Models.Product;

namespace ProductAPI.Data.Repositories.Product;

public interface IProductRepository : IRepositoryBase<CommonProduct>
{
    Task<ProductReturnDto?> GetProductByIdAsync(int productId);
    Task<IEnumerable<ProductReturnDto>> GetAllProductsAsync();
}
