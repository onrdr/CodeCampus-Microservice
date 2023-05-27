using WebUI.Models.Discounts;

namespace WebUI.Services.Interfaces;

public interface IDiscountService
{
    Task<DiscountViewModel> GetDiscount(string discountCode);
}