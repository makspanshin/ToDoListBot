using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ToDoListBot.DAL;
using ToDoListBot.Model;
using ToDoListManagement.Storage;


namespace ToDoListUnitTest;

public class StorageTasksUnitTests
{
    [Test]
    public void AddToDoListItem_Correctly()
    {
        var mockToDoItem = new Mock<DbSet<ToDoItem>>();
        var mockUser = new Mock<DbSet<User>>();
        var users = new List<User>()
        {
            new()
            {
                UserId = 0,
                NickName = "Test",
                Tasks = null
            }
        }.AsQueryable();

        var toDoItems = new List<ToDoItem>().AsQueryable();

        mockToDoItem.As<IQueryable<ToDoItem>>().Setup(m => m.Provider).Returns(toDoItems.Provider);
        mockToDoItem.As<IQueryable<ToDoItem>>().Setup(m => m.Expression).Returns(toDoItems.Expression);
        mockToDoItem.As<IQueryable<ToDoItem>>().Setup(m => m.ElementType).Returns(toDoItems.ElementType);
        mockToDoItem.As<IQueryable<ToDoItem>>().Setup(m => m.GetEnumerator()).Returns(() => toDoItems.GetEnumerator());

        mockUser.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
        mockUser.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
        mockUser.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
        mockUser.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => users.GetEnumerator());

        var mockContext = new Mock<ApplicationContext>();
        mockContext.Setup(c => c.ToDoItem).Returns(mockToDoItem.Object);
        mockContext.Setup(d => d.Users).Returns(mockUser.Object);

        var storage = new StorageTasks(mockContext.Object);

        storage.Add(users.Last().NickName, "testtest");

        var listTasks = storage.GetTasks(users.Last().NickName);

        listTasks.Should().NotBeEmpty();
    }

    [Test]
    public void RemoveToDoListItem_Correctly()
    {
        var mockToDoItem = new Mock<DbSet<ToDoItem>>();
        var mockUser = new Mock<DbSet<User>>();
        var users = new List<User>()
        {
            new()
            {
                UserId = 0,
                NickName = "Test",
                Tasks = null
            }
        }.AsQueryable();

        var toDoItems = new List<ToDoItem>().AsQueryable();

        mockToDoItem.As<IQueryable<ToDoItem>>().Setup(m => m.Provider).Returns(toDoItems.Provider);
        mockToDoItem.As<IQueryable<ToDoItem>>().Setup(m => m.Expression).Returns(toDoItems.Expression);
        mockToDoItem.As<IQueryable<ToDoItem>>().Setup(m => m.ElementType).Returns(toDoItems.ElementType);
        mockToDoItem.As<IQueryable<ToDoItem>>().Setup(m => m.GetEnumerator()).Returns(() => toDoItems.GetEnumerator());

        mockUser.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
        mockUser.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
        mockUser.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
        mockUser.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => users.GetEnumerator());

        var mockContext = new Mock<ApplicationContext>();
        mockContext.Setup(c => c.ToDoItem).Returns(mockToDoItem.Object);
        mockContext.Setup(d => d.Users).Returns(mockUser.Object);

        var storage = new StorageTasks(mockContext.Object);
        
        var item = new ToDoItem("testtest");

        storage.Add(users.Last().NickName, item.Description);

        storage.FinishTask(users.Last().NickName,0);

        var listTasks = storage.GetTasks(users.Last().NickName);

        listTasks.Should().BeEmpty();
    }
}