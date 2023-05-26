using Services.Basket.Dtos;
using Shared.Dtos;

namespace Services.Basket.Services;

public interface IBasketService
{
    Task<Response<BasketDto>> GetBasket(string userId);

    Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);

    Task<Response<bool>> Delete(string userId);
}