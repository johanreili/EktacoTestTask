using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Api.Configuration.Swagger.Examples;
using ProductAPI.Common.Dtos.Product;
using ProductAPI.Common.Utilities;
using ProductAPI.Core.Services.Product;
using Swashbuckle.AspNetCore.Filters;

namespace ProductAPI.Api.Controllers;

/// <summary>
/// Product controller
/// </summary>
[Route("[controller]")]
[ApiController]
public class ProductsController(
    IProductService productService,
    IValidator<ProductIdGetDto> productIdGetDtoValidator,
    IValidator<ProductCreateDto> productCreateDtoValidator
) : ControllerBase
{
    private readonly IProductService _productService = productService;
    private readonly IValidator<ProductIdGetDto> _productIdGetDtoValidator =
        productIdGetDtoValidator;
    private readonly IValidator<ProductCreateDto> _productCreateDtoValidator =
        productCreateDtoValidator;

    #region Summary
    /// <summary>
    /// Adds a new product.
    /// </summary>
    /// <param name="productDto">The product details to add.</param>
    /// <returns>A response indicating the result of the operation.</returns>
    /// <response code="200">Product was added successfully.</response>
    /// <response code="400">Validation failed or a business rule violation occurred.</response>
    /// <response code="500">An unexpected error occurred during the operation.</response>
    #endregion
    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ProductCreatedExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidationErrorExample))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServerErrorExample))]
    public async Task<IActionResult> AddProductAsync([FromBody] ProductCreateDto productDto)
    {
        // DATA VALIDATION
        ValidationResult validationResult = await _productCreateDtoValidator.ValidateAsync(
            productDto
        );
        if (!validationResult.IsValid)
            return BadRequest(validationResult.ValidationFailure());

        // Add product
        var result = await _productService.AddProductAsync(productDto);
        return result.IsSuccess
            ? Ok(Result.Success())
            : BadRequest(Result.Failure(result.Error, result.ErrorCode));
    }

    #region Summary
    /// <summary>
    /// Retrieves a product by its unique ID or retrieves all products if no ID is provided.
    /// </summary>
    /// <param name="productIdGetDto">
    /// The ID of the product to fetch (optional). If not provided, all products will be returned.
    /// </param>
    /// <returns>
    /// A single product if an ID is provided, or a list of all products if no ID is given.
    /// </returns>
    /// <response code="200">
    /// Returns the requested product or a list of all products.
    /// </response>
    /// <response code="400">
    /// Validation failed for the provided ID.
    /// </response>
    /// <response code="404">The specified product was not found.</response>
    /// <response code="500">An unexpected error occurred during the operation.</response>
    #endregion
    [HttpGet]
    [ProducesResponseType(typeof(Result<IEnumerable<ProductReturnDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<ProductReturnDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ProductListExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidationErrorExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundErrorExample))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServerErrorExample))]
    public async Task<IActionResult> GetProductAsync([FromQuery] ProductIdGetDto? productIdGetDto)
    {
        if (productIdGetDto == null || !productIdGetDto.ProductId.HasValue)
        {
            // If no ProductId is provided, return all products
            var result = await _productService.GetAllProductsAsync();
            return result.IsSuccess
                ? Ok(result)
                : BadRequest(Result.Failure(result.Error, result.ErrorCode));
        }

        // DATA VALIDATION
        ValidationResult validationResult = await _productIdGetDtoValidator.ValidateAsync(
            productIdGetDto
        );
        if (!validationResult.IsValid)
            return BadRequest(validationResult.ValidationFailure());

        // GET the product by ID
        var productResult = await _productService.GetProductByIdAsync(
            productIdGetDto.ProductId.Value
        );

        if (!productResult.IsSuccess)
            return productResult.Error == "Product not found."
                ? NotFound(Result.Failure(productResult.Error, "NOT_FOUND"))
                : BadRequest(Result.Failure(productResult.Error, productResult.ErrorCode));

        return Ok(productResult);
    }
}
