using ClimateSenseMAUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimateSenseMAUI.View;

public partial class DashboardPage : ContentPage
{
    private DashboardViewModel _viewModel;

    public DashboardPage(DashboardViewModel vm)
    {
        _viewModel = vm;
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        await _viewModel.StartSubscribingToMqtt();
    }
}