using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoListBot.DAL;
using ToDoListBot.Model;
using System.Collections.Generic;

namespace ToDoListManagement.Storage
{
    public class StorageTasks : IStorageTasks, IDisposable
    {
        private ApplicationContext _dbContext;

        public StorageTasks(ApplicationContext db)
        {
            _dbContext = db;

        }

        public void Add(string NickName, string? Description)
        {
            var currentUser = _dbContext.Users.Where(x => x.NickName == NickName).FirstOrDefault();

            if (currentUser is not null)
                _dbContext.ToDoItem.Add(new ToDoItem()
                { Description = Description, DateCreate = DateTime.Now.ToUniversalTime(), UserId = currentUser.UserId });
            else
            {
                //TODO обработка когда пользователь не найден
            }
            _dbContext.SaveChanges();
        }

        public void Remove(ToDoItem item)
        {
           // items.Remove(item);
        }

        public List<ToDoItem> GetTasks(string NickName)
        {
            var currentUser = _dbContext.Users.Where(x => x.NickName == NickName).FirstOrDefault();
            if (currentUser is not null)
                return _dbContext.ToDoItem.Where(x => x.UserId == currentUser.UserId).OrderBy(i => i.DateCreate)
                    .ToList();
            else
            {
                _dbContext.Users.Add(new User() {NickName = NickName});
                _dbContext.SaveChanges();
            }
            return null;
        }


        public void Dispose()
        {
            if (_dbContext is not null)
            {
                _dbContext.Dispose();
            }
        }
    }
}
