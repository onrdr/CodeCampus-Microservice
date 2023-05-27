using IdentityModel.Client;
using Shared.Dtos;
using WebUI.Models;

namespace WebUI.Services.Interfaces;

public interface IIdentityService
{
    Task<Response<bool>> SignIn(SigninInput signinInput);

    Task<TokenResponse> GetAccessTokenByRefreshToken();

    Task RevokeRefreshToken();
}