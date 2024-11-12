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
        List<TodoItem> GetTodos();

        Task Save(TodoItem item);
    }
}
