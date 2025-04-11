using FluentValidation;
using ProductAPI.Common.Dtos.Product;

namespace ProductAPI.Core.Validators;

public class ProductGetDtoValidator : AbstractValidator<ProductIdGetDto>
{
    public ProductGetDtoValidator()
    {
        RuleFor(dto => dto.ProductId)
            .GreaterThan(0)
            .WithMessage("ProductId must be a positive number.");
    }
}
