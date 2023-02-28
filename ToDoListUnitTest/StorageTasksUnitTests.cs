using FluentAssertions;
using NUnit.Framework;
using ToDoListManager;
using ToDoListManager.Models;

namespace ToDoListUnitTest;

public class StorageTasksUnitTests
{
    [Test]
    public void AddToDoListItem_Correctly()
    {
        var storage = new StorageTasks();

        var item = new ToDoItem("test", "testtest", DateTime.Now);

        storage.Add(item);

        var listTasks = storage.GetTasks();

        listTasks.Should().NotBeEmpty();
    }

    [Test]
    public void RemoveToDoListItem_Correctly()
    {
        var storage = new StorageTasks();

        var item = new ToDoItem("test", "testtest", DateTime.Now);

        storage.Add(item);

        storage.Remove(item);

        var listTasks = storage.GetTasks();

        listTasks.Should().BeEmpty();
    }
}