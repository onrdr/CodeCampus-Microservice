using WebUI.Models;

namespace WebUI.Services.Interfaces;

public interface IUserService
{
    Task<UserViewModel> GetUser();
}