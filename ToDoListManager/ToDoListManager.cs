using ToDoListBot.Model;
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

        public void Add(string NickName, string? Description)
        {
            _storageTasks.Add(NickName,Description);
        }

        //TODO подумать как удалять
        public void Delete(int index)
        {
          //  _storageTasks.GetTasks().RemoveAt(index);
        }

        public IEnumerable<ToDoItem> GetAllTasks(string NickName)
        {
            return _storageTasks.GetTasks(NickName);
        }

        public void СompleteTask(int index)
        {
            //_storageTasks.GetTasks().ElementAt(index).IsDone = true;
        }
    }
}