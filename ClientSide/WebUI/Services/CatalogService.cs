using Shared.Dtos;
using WebUI.Helpers;
using WebUI.Models.Catalogs;
using WebUI.Services.Interfaces;

namespace WebUI.Services;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _client;
    private readonly IPhotoStockService _photoStockService;
    private readonly PhotoHelper _photoHelper;

    public CatalogService(
        HttpClient client, 
        IPhotoStockService photoStockService, 
        PhotoHelper photoHelper)
    {
        _client = client;
        _photoStockService = photoStockService;
        _photoHelper = photoHelper;
    }

    public async Task<List<CourseViewModel>> GetAllCourseAsync()
    {
        //http:localhost:5000/services/catalog/courses
        var response = await _client.GetAsync("courses");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
        responseSuccess.Data.ForEach(x =>
        {
            x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
        });
        return responseSuccess.Data;
    }

    public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
    {
        var response = await _client.GetAsync("categories");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

        return responseSuccess.Data;
    }

    public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
    {
        //[controller]/GetAllByUserId/{userId}

        var response = await _client.GetAsync($"courses/GetAllByUserId/{userId}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

        responseSuccess.Data.ForEach(x =>
        {
            x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
        });

        return responseSuccess.Data;
    }

    public async Task<CourseViewModel> GetByCourseId(string courseId)
    {
        var response = await _client.GetAsync($"courses/{courseId}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

        responseSuccess.Data.StockPictureUrl = _photoHelper.GetPhotoStockUrl(responseSuccess.Data.Picture);

        return responseSuccess.Data;
    }

    public async Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
    {
        var resultPhotoService = await _photoStockService.UploadPhoto(courseCreateInput.PhotoFormFile);

        if (resultPhotoService != null)
        {
            courseCreateInput.Picture = resultPhotoService.Url;
        }

        var response = await _client.PostAsJsonAsync("courses", courseCreateInput);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
    {
        var resultPhotoService = await _photoStockService.UploadPhoto(courseUpdateInput.PhotoFormFile);

        if (resultPhotoService != null)
        {
            await _photoStockService.DeletePhoto(courseUpdateInput.Picture);
            courseUpdateInput.Picture = resultPhotoService.Url;
        }

        var response = await _client.PutAsJsonAsync("courses", courseUpdateInput);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCourseAsync(string courseId)
    {
        var response = await _client.DeleteAsync($"courses/{courseId}");

        return response.IsSuccessStatusCode;
    }
}