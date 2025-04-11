using FluentValidation;
using ProductAPI.Common.Dtos.Product;
using ProductAPI.Core.Services.ProductGroup;
using ProductAPI.Data.Repositories.Store;

namespace ProductAPI.Common.Validators;

public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateDtoValidator(
        IProductGroupService productGroupService,
        IStoreService storeService
    )
    {
        var maxGroupId = productGroupService.GetMaxProductGroupIdAsync().GetAwaiter().GetResult();

        // Name is required and have a maximum length
        RuleFor(product => product.ProductName)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .MaximumLength(255)
            .WithMessage("Product name can not be more than 255 characters long.");

        // GroupId must be greater than 0 and less than or equal to the max ProductGroup ID
        RuleFor(product => product.ProductGroupId)
            .NotEmpty()
            .WithMessage("ProductGroupId is required.")
            .GreaterThan(0)
            .WithMessage("ProductGroupId must be a positive number.")
            .Must(groupId => groupId <= maxGroupId)
            .WithMessage(
                $"ProductGroupId must not exceed the maximum ProductGroup ID: {maxGroupId}"
            );

        // At least two of SalePrice, SalePriceWithVAT, and VATRate must be provided
        RuleFor(product => product)
            .Must(HaveAtLeastTwoPriceFields)
            .WithMessage(
                "At least two of SalePrice, SalePriceWithVAT, and VATRate must be provided."
            );

        // SalePrice must be positive if provided
        RuleFor(product => product.SalePrice)
            .GreaterThan(-1)
            .When(product => product.SalePrice.HasValue)
            .WithMessage("Sale price must be a positive value.");

        // SalePriceWithVAT must be positive if provided
        RuleFor(product => product.SalePriceWithVAT)
            .GreaterThan(-1)
            .When(product => product.SalePriceWithVAT.HasValue)
            .WithMessage("Sale price with VAT must be a positive value.");

        // VATRate must be a percentage between 0 and 100 if provided
        RuleFor(product => product.VATRate)
            .InclusiveBetween(0, 100)
            .When(product => product.VATRate.HasValue)
            .WithMessage("VAT rate must be between 0 and 100.");

        // StoreIds must all exist in the database
        RuleFor(product => product.StoreIds)
            .MustAsync(
                async (storeIds, cancellationToken) =>
                {
                    if (storeIds == null || storeIds.Count == 0)
                    {
                        return true; // If no StoreIds are provided, it's valid
                    }

                    return await storeService.DoStoresExistAsync(storeIds);
                }
            )
            .WithMessage("One or more StoreIds are invalid.");

        // Business rule: make sure that the prices and VAT rate make logical sense if all three are provided
        RuleFor(product => product)
            .Must(product =>
            {
                if (
                    product.SalePrice.HasValue
                    && product.SalePriceWithVAT.HasValue
                    && product.VATRate.HasValue
                )
                {
                    var expectedSalePriceWithVAT =
                        product.SalePrice.Value * (1 + product.VATRate.Value / 100);

                    // Validate that SalePriceWithVAT is correctly calculated
                    if (
                        Math.Abs(
                            (decimal)(product.SalePriceWithVAT.Value - expectedSalePriceWithVAT)
                        ) > 0.01m
                    )
                    {
                        return false;
                    }

                    // SalePriceWithVAT must not be lower than SalePrice
                    if (product.SalePriceWithVAT.Value < product.SalePrice.Value)
                    {
                        return false;
                    }
                }

                return true;
            })
            .WithMessage(
                "If provided, SalePrice, SalePriceWithVAT, and VATRate must be consistent. SalePriceWithVAT must equal SalePrice + VAT and must not be lower than SalePrice."
            );

        // Business rule: If only SalePrice and SalePriceWithVAT are provided, SalePriceWithVAT can not be lower than SalePrice
        RuleFor(product => product)
            .Must(product =>
            {
                if (
                    product.SalePrice.HasValue
                    && product.SalePriceWithVAT.HasValue
                    && !product.VATRate.HasValue
                )
                {
                    return product.SalePriceWithVAT.Value >= product.SalePrice.Value;
                }

                return true;
            })
            .WithMessage("SalePriceWithVAT can not be lower than SalePrice.");

        // Business rule: Ensure the calculated VAT rate does not exceed 100%
        RuleFor(product => product)
            .Must(product =>
            {
                if (
                    product.SalePrice.HasValue
                    && product.SalePriceWithVAT.HasValue
                    && !product.VATRate.HasValue
                )
                {
                    var calculatedVATRate =
                        (product.SalePriceWithVAT.Value / product.SalePrice.Value - 1) * 100;

                    return calculatedVATRate <= 100;
                }

                return true;
            })
            .WithMessage("The calculated VAT rate must not exceed 100%.");
    }

    private bool HaveAtLeastTwoPriceFields(ProductCreateDto product)
    {
        return new[] { product.SalePrice, product.SalePriceWithVAT, product.VATRate }.Count(
                value => value.HasValue
            ) >= 2;
    }
}
