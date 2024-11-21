using System.Net.Http.Headers;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;

namespace ClimateSenseWeb.Middleware.HttpClientHandlers;

public class TokenHandler(IHttpContextAccessor contextAccessor) : DelegatingHandler
{

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (contextAccessor.HttpContext == null)
        {
            return await base.SendAsync(request, cancellationToken);
        }
        string? token = await contextAccessor.HttpContext.GetTokenAsync("access_token");

        request.Headers.Authorization = new AuthenticationHeaderValue(OidcConstants.TokenRequestTypes.Bearer, token);

        return await base.SendAsync(request, cancellationToken);
    }
}