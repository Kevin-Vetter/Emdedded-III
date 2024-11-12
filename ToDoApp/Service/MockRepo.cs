using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Service
{
    public class MockRepo(IRestService restService) : IToDoService
    {

        public Task<List<TodoItem>> GetTasksAsync()
        {
            return restService.RefreshDataAsync();
        }

        public Task SaveTaskAsync(TodoItem item, bool isNewItem = false)
        {
            return restService.SaveTodoItemAsync(item, isNewItem);
        }

        public Task DeleteTaskAsync(TodoItem item)
        {
            return restService.DeleteTodoItemAsync(item.Id);
        }
    }
}
