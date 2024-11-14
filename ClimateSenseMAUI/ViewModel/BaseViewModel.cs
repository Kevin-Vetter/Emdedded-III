using CommunityToolkit.Mvvm.ComponentModel;


namespace ClimateSenseMAUI.ViewModel;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty] private bool isRefreshing = false;

    [ObservableProperty] bool isBusy = false;

    [ObservableProperty] string title = "";
}