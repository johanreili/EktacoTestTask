using AutoMapper;
using ProductAPI.Common.Dtos.Product;
using ProductAPI.Common.Utilities;
using ProductAPI.Core.Services.ProductStore;
using ProductAPI.Data.Repositories.Product;
using CommonProduct = ProductAPI.Common.Models.Product;

namespace ProductAPI.Core.Services.Product;

public class ProductService(
    IProductRepository productRepository,
    IProductStoreService productStoreService,
    IMapper mapper
) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IProductStoreService _productStoreService = productStoreService;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> AddProductAsync(ProductCreateDto productDto)
    {
        try
        {
            // Calculate price-related fields
            if (productDto.SalePrice.HasValue && productDto.VATRate.HasValue)
            {
                productDto.SalePriceWithVAT =
                    productDto.SalePrice * (1 + productDto.VATRate.Value / 100);
            }
            else if (productDto.SalePriceWithVAT.HasValue && productDto.VATRate.HasValue)
            {
                productDto.SalePrice =
                    productDto.SalePriceWithVAT / (1 + productDto.VATRate.Value / 100);
            }
            else if (productDto.SalePrice.HasValue && productDto.SalePriceWithVAT.HasValue)
            {
                productDto.VATRate = (productDto.SalePriceWithVAT / productDto.SalePrice - 1) * 100;
            }

            var product = _mapper.Map<CommonProduct>(productDto);
            product.DateAdded = DateTime.UtcNow;

            // Save the product
            var savedProduct = await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            // Add product to stores if provided
            if (productDto.StoreIds != null && productDto.StoreIds.Count > 0)
            {
                var storeResult = await _productStoreService.AddProductToStoreAsync(
                    savedProduct.ProductId,
                    productDto.StoreIds
                );
                if (!storeResult.IsSuccess)
                {
                    return Result.Failure(storeResult.Error);
                }
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"An error occurred while adding the product: {ex.Message}");
        }
    }

    public async Task<Result<ProductReturnDto>> GetProductByIdAsync(int productId)
    {
        try
        {
            var product = (await _productRepository.GetProductByIdAsync(productId));

            if (product == null)
                return Result<ProductReturnDto>.Failure("Product not found.");

            return Result<ProductReturnDto>.Success(product);
        }
        catch (Exception ex)
        {
            return Result<ProductReturnDto>.Failure($"An error occurred: {ex.Message}");
        }
    }

    public async Task<Result<IEnumerable<ProductReturnDto>>> GetAllProductsAsync()
    {
        try
        {
            var products = await _productRepository.GetAllProductsAsync();
            return Result<IEnumerable<ProductReturnDto>>.Success(
                _mapper.Map<IEnumerable<ProductReturnDto>>(products)
            );
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<ProductReturnDto>>.Failure(
                $"An error occurred while fetching products: {ex.Message}"
            );
        }
    }
}
