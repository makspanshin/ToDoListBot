using ToDoListBot.Model;

namespace ToDoListManagement.Storage;

public interface IStorageTasks
{
    void Add(string NickName, string? Description);
    List<ToDoItem> GetTasks(string NickName);
    void FinishTask(string NickName, int indexTask);

}