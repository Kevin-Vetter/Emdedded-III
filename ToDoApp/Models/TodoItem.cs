using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string? Head { get; set; }
        public string? Body { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Completed { get; set; }
    }
}
