using FluentValidation;
using WebUI.Models.Discounts;

namespace WebUI.Validators;

public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
{
    public DiscountApplyInputValidator()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("indirim kupon alanı boş olamaz");
    }
}