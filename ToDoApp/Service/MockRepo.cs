using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Service
{
    public class MockRepo : IToDoService
    {
        private IEnumerable<TodoItem> items { get; set; }
        public MockRepo()
        {
            items = new List<TodoItem>()
            {
                new TodoItem { Id = 1, Description = "1", Completed = false, CreateTime = DateTime.Now, Priority = PriorityLevel.Low },
                new TodoItem { Id = 2, Description = "2", Completed = true, CreateTime = DateTime.Now, Priority = PriorityLevel.High },
                new TodoItem { Id = 3, Description = "3", Completed = true, CreateTime = DateTime.Now, Priority = PriorityLevel.Medium },
                new TodoItem { Id = 4, Description = "4", Completed = false, CreateTime = DateTime.Now, Priority = PriorityLevel.Low },
            };
        }
        public IEnumerable<TodoItem> GetTodos()
        {
            return items;
        }
    }
}
