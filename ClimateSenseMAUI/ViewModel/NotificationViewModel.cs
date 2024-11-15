using System.Collections.ObjectModel;
using ClimateSenseMAUI.View;
using ClimateSenseModels;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace ClimateSenseMAUI.ViewModel;

public partial class NotificationViewModel : IRecipient<Notification>
{
    public ObservableCollection<Notification> Notifications { get; } = new();

    public NotificationViewModel()
    {
        WeakReferenceMessenger.Default.Register(this);
    }

    public void Receive(Notification message)
    {
        Notifications.Add(message);
    }

    [RelayCommand]
    async Task OpenNotificationPage()
    {
        await Shell.Current.Navigation.PushModalAsync(new NotificationPage(this), true);
    }

    [RelayCommand]
    async Task CloseNotificationPage()
    {
        await Shell.Current.Navigation.PopModalAsync(true);
    }
}