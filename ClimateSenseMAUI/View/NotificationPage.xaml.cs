using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClimateSenseMAUI.ViewModel;

namespace ClimateSenseMAUI.View;

public partial class NotificationPage : ContentPage
{
    public NotificationPage(NotificationViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}