using Microsoft.AspNetCore.Mvc;
using ProductAPI.Api.Configuration.Swagger.Examples;
using ProductAPI.Common.Dtos.ProductGroup;
using ProductAPI.Common.Utilities;
using ProductAPI.Core.Services.ProductGroup;
using Swashbuckle.AspNetCore.Filters;

namespace ProductAPI.Api.Controllers;

/// <summary>
/// Product Groups controller
/// </summary>
[Route("[controller]")]
[ApiController]
public class ProductGroupsController(IProductGroupService productGroupService) : ControllerBase
{
    private readonly IProductGroupService _productGroupService = productGroupService;

    #region Summary
    /// <summary>
    /// Retrieves all product groups in a tree structure with their child groups.
    /// </summary>
    /// <returns>A tree structure of product groups.</returns>
    /// <response code="200">Successfully retrieved the product group tree.</response>
    /// <response code="500">An unexpected error occurred while fetching the product group tree.</response>
    #endregion
    [HttpGet]
    [ProducesResponseType(
        typeof(Result<IEnumerable<ProductGroupReturnAsTreeDto>>),
        StatusCodes.Status200OK
    )]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ProductGroupTreeExample))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServerErrorExample))]
    public async Task<IActionResult> GetProductGroupTree()
    {
        var result = await _productGroupService.GetProductGroupTreeAsync();
        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(Result.Failure(result.Error, result.ErrorCode));
    }
}
