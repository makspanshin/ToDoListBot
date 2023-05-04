using ToDoListBot.Model;

namespace ToDoListManagement.Storage;

public interface IStorageTasks
{
    void Add(string nickName, string? description);
    List<ToDoItem> GetTasks(string nickName);
    void FinishTask(string nickName, int indexTask);
}