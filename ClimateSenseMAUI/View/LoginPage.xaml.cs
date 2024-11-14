using ClimateSenseMAUI.ViewModel;

namespace ClimateSenseMAUI.View;

public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel _vm;

    public LoginPage(LoginViewModel vm) 
	{
		InitializeComponent();
        _vm = vm;
		BindingContext = _vm;
	}

    protected override void OnAppearing()
    {
        _vm.CheckLoggedIn();
    }
}