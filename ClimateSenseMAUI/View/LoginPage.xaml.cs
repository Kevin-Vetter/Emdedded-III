using ClimateSenseMAUI.ViewModel;

namespace ClimateSenseMAUI.View;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel vm) 
	{
		InitializeComponent();
		BindingContext = vm;
	}
}