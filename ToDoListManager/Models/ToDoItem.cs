using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListManagement.Models
{
    public class ToDoItem
    {
        public string ShortName { get; }
        public string Description { get; }
        public DateTime? DateCreate { get; }
        public bool IsDone { get; set; }
        public ToDoItem(string shortName, string description)
        {
            ShortName = shortName;
            Description = description;
            DateCreate = DateTime.Now;
        }
    }
}
