using WebUI.Models.PhotoStocks;

namespace WebUI.Services.Interfaces;

public interface IPhotoStockService
{
    Task<PhotoViewModel> UploadPhoto(IFormFile photo);

    Task<bool> DeletePhoto(string photoUrl);
}