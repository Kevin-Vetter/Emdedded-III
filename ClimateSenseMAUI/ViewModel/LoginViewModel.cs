using CommunityToolkit.Mvvm.Input;
using Auth0.OidcClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ClimateSenseMAUI.ViewModel
{
    public partial class LoginViewModel(Auth0Client auth0Client) : BaseViewModel
    {

        [RelayCommand]
        public async Task LoginAsync()
        {

            var loginResult = await auth0Client.LoginAsync();

            int x = 0;
            if (!loginResult.IsError)
                x = 1;// await shell go to mainpage/dashboard
            else
                await Shell.Current.DisplayAlert("Login Error!", $"Failed to authenticate.\n{loginResult.ErrorDescription}", "OK");
        }
    }
}
