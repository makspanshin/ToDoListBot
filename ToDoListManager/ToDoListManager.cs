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

    public void Add(string NickName, string? Description)
    {
        _storageTasks.Add(NickName, Description);
    }

    public IEnumerable<ToDoItem> GetAllTasks(string NickName)
    {
        return _storageTasks.GetTasks(NickName);
    }

    public void СompleteTask(string NickName, int indexTask)
    {
        _storageTasks.FinishTask(NickName, indexTask);
    }
}