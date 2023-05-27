using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net;
using System.Net.Http.Headers;
using WebUI.Exceptions;
using WebUI.Services.Interfaces;

namespace WebUI.Handlers;

public class ResourceOwnerPasswordTokenHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IIdentityService _identityService;
    private readonly ILogger<ResourceOwnerPasswordTokenHandler> _logger;

    public ResourceOwnerPasswordTokenHandler(
        IHttpContextAccessor httpContextAccessor,
        IIdentityService identityService,
        ILogger<ResourceOwnerPasswordTokenHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _identityService = identityService;
        _logger = logger;
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var tokenResponse = await _identityService.GetAccessTokenByRefreshToken();

            if (tokenResponse != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

                response = await base.SendAsync(request, cancellationToken);
            }
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            throw new UnAuthorizeException();
        }

        return response;
    }
}