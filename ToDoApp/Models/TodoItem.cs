using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Models
{
    public class TodoItem
    {
        public int Id {  get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public PriorityLevel Priority {  get; set; }
        public bool Completed { get; set; }


    }
}
