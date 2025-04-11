using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Common.Dtos.Product;
using ProductAPI.Data.Data;
using ProductAPI.Data.Repositories.Base;
using CommonProduct = ProductAPI.Common.Models.Product;

namespace ProductAPI.Data.Repositories.Product;

public class ProductRepository(ApplicationDbContext context, IMapper mapper)
    : RepositoryBase<CommonProduct>(context),
        IProductRepository
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<ProductReturnDto?> GetProductByIdAsync(int productId)
    {
        var product = await _dbSet
            .Where(p => p.ProductId == productId)
            .Include(p => p.ProductGroup)
            .Include(p => p.ProductStores)
            .ThenInclude(ps => ps.Store)
            .ProjectTo<ProductReturnDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return product; // Return null if not found
    }

    public async Task<IEnumerable<ProductReturnDto>> GetAllProductsAsync()
    {
        return await _dbSet
            .ProjectTo<ProductReturnDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
