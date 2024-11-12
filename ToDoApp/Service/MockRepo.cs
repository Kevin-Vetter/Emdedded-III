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
        private List<TodoItem> items { get; set; }
        public MockRepo()
        {
            items = new List<TodoItem>()
            {
                new TodoItem { Id = 1, Description = "Description", Completed = false, CreateTime = DateTime.Now, Priority = PriorityLevel.Low },
                new TodoItem { Id = 2, Description = "Not a description", Completed = true, CreateTime = DateTime.Now, Priority = PriorityLevel.High },
                new TodoItem { Id = 3, Description = "Jeg elsker goth gym mommy", Completed = true, CreateTime = DateTime.Now, Priority = PriorityLevel.Medium },
                new TodoItem { Id = 4, Description = "Max er 23cm ;)", Completed = false, CreateTime = DateTime.Now, Priority = PriorityLevel.Low },
            };
        }
        public List<TodoItem> GetTodos()
        {
            return new(items);
        }

        public async Task Save(TodoItem item)
        {
            throw new NotImplementedException();
        }
    }
}
