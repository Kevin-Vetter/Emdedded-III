using System.Security.Claims;
using System.Security.Principal;
using Auth0.OidcClient;
using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClimateSenseNative;

public class Auth0AuthenticationStateProvider(Auth0Client auth0Client) : AuthenticationStateProvider
{
    private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(new AuthenticationState(_currentUser));
    }

    public Task LoginAsync()
    {
        Task<AuthenticationState> task = LoginWithAuth0Async();
        NotifyAuthenticationStateChanged(task);

        return task;
    }

    private async Task<AuthenticationState> LoginWithAuth0Async()
    {
        LoginResult result = await auth0Client.LoginAsync(new { audience = Appsettings.Auth0["Audience"] });

        if (!result.IsError)
        {
            IIdentity? identity = result.User.Identity;
            if (identity != null)
            {
                _currentUser = new ClaimsPrincipal(new ClaimsIdentity(identity, result.User.Claims, identity.AuthenticationType, "name", "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")) ;
            }

            TokenHolder.AccessToken = result.AccessToken;
        }

        return new AuthenticationState(_currentUser);
    }

    public async Task LogoutAsync()
    {
        await auth0Client.LogoutAsync();
        _currentUser = new ClaimsPrincipal(new ClaimsPrincipal());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
    }
}