using ToDoListManagement;
using ToDoListManagement.Models;

namespace MyApp;

internal class Program
{
    private static void Main(string[] args)
    {
        var doItem1 = new ToDoItem("Сделать", "To do porgramm");
        var doItem2 = new ToDoItem("Сделать1", "To do porgramm1");
        var doItem3 = new ToDoItem("Сделать2", "To do porgramm2");

        var manager = new ToDoListManager(new StorageTasks());

        manager.Add(doItem1);
        manager.Add(doItem2);
        manager.Add(doItem3);

        var list = manager.GetAllTasks();
        var index = 0;
        foreach (var item in list)
        {
            Console.WriteLine(index + ":" + item.ShortName + ":" + item.Description + "-" + item.IsDone);
            index++;
        }

        Console.WriteLine("------------------------------------------------------------------");
        manager.СompleteTask(2);
        index = 0;
        foreach (var item in list)
        {
            Console.WriteLine(index + ":" + item.ShortName + ":" + item.Description + "-" + item.IsDone);
            index++;
        }

        manager.Delete(2);
        Console.WriteLine("------------------------------------------------------------------");
        index = 0;
        foreach (var item in list)
        {
            Console.WriteLine(index + ":" + item.ShortName + ":" + item.Description + "-" + item.IsDone);
            index++;
        }
    }
}