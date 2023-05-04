using ToDoListBot.Model;
using ToDoListManagement.Storage;

namespace ToDoListManagement;

public class ToDoListManager
{
    private readonly IStorageTasks _storageTasks;

    public ToDoListManager(IStorageTasks storageTasks)
    {
        _storageTasks = storageTasks;
    }

    public void Add(string nickName, string? description)
    {
        _storageTasks.Add(nickName, description);
    }

    public IEnumerable<ToDoItem> GetAllTasks(string nickName)
    {
        return _storageTasks.GetTasks(nickName);
    }

    public void СompleteTask(string nickName, int indexTask)
    {
        _storageTasks.FinishTask(nickName, indexTask);
    }
}