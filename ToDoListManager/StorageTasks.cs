using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListManagement.Models;

namespace ToDoListManagement
{
    public class StorageTasks : IStorageTasks
    {
        private List<ToDoItem> items;

        public StorageTasks()
        {
            items = new List<ToDoItem>();
        }

        public void Add(ToDoItem item)
        {
            items.Add(item);
        }

        public void Remove(ToDoItem item)
        {
            items.Remove(item);
        }

        public List<ToDoItem> GetTasks()
        {
            return items;
        }

    }
}
