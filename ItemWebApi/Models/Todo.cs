using System.ComponentModel.DataAnnotations;

namespace ItemWebApi.Models
{
    public class Todo
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Head { get; set; }
        public string? Body { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Completed { get; set; }
    }
}
