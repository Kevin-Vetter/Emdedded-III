using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Service
{
    public interface IToDoService
    {
        Task<List<TodoItem>> GetTasksAsync();
        Task SaveTaskAsync(TodoItem item, bool isNewItem);
        Task DeleteTaskAsync(TodoItem item);
    }
}
