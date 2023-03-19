using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListBot.Model
{
    public class ToDoItem
    {
        public int ToDoItemId { get; set; }
        public string? Description { get; set; }
        public DateTime? DateCreate { get; set; }
        public bool IsDone { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public ToDoItem(string description)
        {
            Description = description;
            DateCreate = DateTime.Now;
        }
        public ToDoItem()
        {
        }
    }
}
