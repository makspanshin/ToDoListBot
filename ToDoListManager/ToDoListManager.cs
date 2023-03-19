using ToDoListManagement.Models;
using ToDoListManagement.Storage;

namespace ToDoListManagement
{
    public class ToDoListManager
    {
        private readonly IStorageTasks _storageTasks;

        public ToDoListManager(IStorageTasks storageTasks)
        {
            _storageTasks = storageTasks;
        }

        public void Add(ToDoItem task)
        {
            _storageTasks.Add(task);
        }

        //TODO подумать как удалять
        public void Delete(int index)
        {
            _storageTasks.GetTasks().RemoveAt(index);
        }

        public IEnumerable<ToDoItem> GetAllTasks()
        {
            return _storageTasks.GetTasks();
        }

        public void СompleteTask(int index)
        {
            _storageTasks.GetTasks().ElementAt(index).IsDone = true;
        }
    }
}