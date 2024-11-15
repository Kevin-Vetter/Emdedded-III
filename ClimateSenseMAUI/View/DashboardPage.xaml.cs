using ClimateSenseMAUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimateSenseMAUI.View;

public partial class DashboardPage : ContentPage
{
    private DashboardViewModel _vm;
    public DashboardPage(DashboardViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.GetRoomsCommand.Execute(null);
        
    }
}