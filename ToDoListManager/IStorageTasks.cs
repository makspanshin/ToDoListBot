using ToDoListManagement.Models;

namespace ToDoListManagement;

public interface IStorageTasks
{
    void Add(ToDoItem item);
    void Remove(ToDoItem item);
    public List<ToDoItem> GetTasks();
}