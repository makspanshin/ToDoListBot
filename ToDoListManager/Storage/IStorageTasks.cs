using ToDoListBot.Model;

namespace ToDoListManagement.Storage;

public interface IStorageTasks
{
    void Add(string NickName, string? Description);
    void Remove(ToDoItem item);
    public List<ToDoItem> GetTasks(string NickName);
}