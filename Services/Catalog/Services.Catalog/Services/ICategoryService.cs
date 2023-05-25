using Services.Catalog.Dtos;
using Shared.Dtos;

namespace Services.Catalog.Services;

public interface ICategoryService
{
    Task<Response<List<CategoryDto>>> GetAllAsync();
    Task<Response<CategoryDto>> GetByIdAsync(string id);
    Task<Response<CategoryDto>> CreateAsync(CategoryDto category);
}