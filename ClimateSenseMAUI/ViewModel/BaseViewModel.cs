using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimateSenseMAUI.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isRefreshing = false;

        [ObservableProperty]
        bool isBusy = false;

        [ObservableProperty]
        string title = "";

    }
}
