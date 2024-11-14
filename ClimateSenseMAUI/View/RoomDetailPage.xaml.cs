using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClimateSenseMAUI.ViewModel;

namespace ClimateSenseMAUI.View;

public partial class RoomDetailPage : ContentPage
{
    private RoomDetailViewModel _viewModel;
    public RoomDetailPage(RoomDetailViewModel viewModel)
    {
        InitializeComponent();
        this._viewModel = viewModel;
        BindingContext = viewModel;
    }
    
    protected override void OnAppearing()
    {
        _viewModel.GetRoomCommandCommand.Execute(null);
    }
}