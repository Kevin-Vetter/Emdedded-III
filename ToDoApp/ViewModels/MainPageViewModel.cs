using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Models;
using ToDoApp.Service;

namespace ToDoApp.ViewModels
{
    public partial class MainPageViewModel(IToDoService service) : BaseViewModel
    {
        readonly IToDoService service = service;
        public ObservableCollection<TodoItem> Items { get; set; } = new ObservableCollection<TodoItem>();


        [RelayCommand]
        async Task GetItems()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                IsRefreshing = true;

                var items = service.GetTodos();
                List<int> tal = new();
                if (Items.Count != 0)
                    Items.Clear();

                foreach (var item in items)
                    Items.Add(item);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
    }
}
