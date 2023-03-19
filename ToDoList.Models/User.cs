using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListBot.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string NickName { get; set; }
        public virtual List<ToDoItem> Tasks { get; set; }
    }
}
