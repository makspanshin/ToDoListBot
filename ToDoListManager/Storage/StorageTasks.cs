using ToDoListBot.DAL;
using ToDoListBot.Model;

namespace ToDoListManagement.Storage;

public class StorageTasks : IStorageTasks, IDisposable
{
    private readonly ApplicationContext _dbContext;

    public StorageTasks(ApplicationContext db)
    {
        _dbContext = db;
    }


    public void Dispose()
    {
        if (_dbContext is not null) _dbContext.Dispose();
    }

    public void Add(string nickName, string? description)
    {
        var currentUser = _dbContext.Users.FirstOrDefault(x => x.NickName == nickName);

        if (currentUser is not null)
        {
            _dbContext.ToDoItem.Add(new ToDoItem
                {Description = description, DateCreate = DateTime.Now.ToUniversalTime(), UserId = currentUser.UserId});
        }

        _dbContext.SaveChanges();
    }

    public List<ToDoItem> GetTasks(string nickName)
    {
        var currentUser = _dbContext.Users.FirstOrDefault(x => x.NickName == nickName);
        if (currentUser is not null)
            return _dbContext.ToDoItem.Where(x => x.UserId == currentUser.UserId).OrderBy(i => i.DateCreate)
                .ToList();
        _dbContext.Users.Add(new User {NickName = nickName});
        _dbContext.SaveChanges();
        return null;
    }

    public void FinishTask(string nickName, int indexTask)
    {
        var currentUser = _dbContext.Users.FirstOrDefault(x => x.NickName == nickName);
        if (currentUser is not null)
        {
            var deleteItem = _dbContext.ToDoItem.Where(x => x.UserId == currentUser.UserId).ToList()
                .OrderBy(i => i.DateCreate)
                .ElementAtOrDefault(indexTask);

            if (deleteItem is not null)
            {
                _dbContext.ToDoItem.Remove(deleteItem);
                _dbContext.SaveChanges();
            }
        }
    }
}