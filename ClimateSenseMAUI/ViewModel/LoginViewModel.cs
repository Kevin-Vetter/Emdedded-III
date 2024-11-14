using CommunityToolkit.Mvvm.Input;
using Auth0.OidcClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClimateSenseMAUI.View;
using IdentityModel.OidcClient;
using System.IdentityModel.Tokens.Jwt;

namespace ClimateSenseMAUI.ViewModel
{
    public partial class LoginViewModel(Auth0Client auth0Client) : BaseViewModel
    {
        public async Task CheckLoggedIn()
        {
          var token = await SecureStorage.GetAsync("access_token");
            var handler = new JwtSecurityTokenHandler();

            if (handler.CanReadToken(token))
            {
                await Shell.Current.GoToAsync(nameof(DashboardPage), true);
            }
        }

        [RelayCommand]
        public async Task LoginAsync()
        {

            var loginResult = await auth0Client.LoginAsync();

            if (!loginResult!.IsError)
            {
                await SecureStorage.SetAsync("access_token", loginResult.AccessToken);
                await Shell.Current.GoToAsync(nameof(DashboardPage), true);
            }
            else
                await Shell.Current.DisplayAlert("Login Error!", $"Failed to authenticate.\n{loginResult.ErrorDescription}", "OK");
        }
    }
}
