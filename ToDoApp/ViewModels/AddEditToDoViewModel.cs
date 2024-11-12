using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Models;
using ToDoApp.Service;
using ToDoApp.Views;

namespace ToDoApp.ViewModels
{


    [QueryProperty(nameof(Item), "item")]
    public partial class AddEditToDoViewModel(IToDoService service) : BaseViewModel
    {
        [ObservableProperty]
        TodoItem item;

        bool isNewItem;

        void OnTodoItemChanging(TodoItem value)
        {
            isNewItem = string.IsNullOrWhiteSpace(value.CreateTime.ToString()) ? true : false;
        }

        [RelayCommand]
        async Task Save()
        {
            await service.SaveTaskAsync(Item, isNewItem);
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task Delete()
        {
            await service.DeleteTaskAsync(Item);
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
