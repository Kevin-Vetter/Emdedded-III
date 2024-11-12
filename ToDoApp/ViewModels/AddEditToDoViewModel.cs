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


    [QueryProperty(nameof(TodoItem), "item")]
    public partial class AddEditToDoViewModel(IToDoService service) : BaseViewModel
    {
        [ObservableProperty]
        TodoItem Item; 
    }
}
