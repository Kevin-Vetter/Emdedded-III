using System.Security.Claims;
using Auth0.OidcClient;
using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClimateSenseMAUI;

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
        LoginResult result = await auth0Client.LoginAsync();

        if (!result.IsError)
        {
            _currentUser = result.User;
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