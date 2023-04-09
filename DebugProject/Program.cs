using ToDoListBot.DAL;
using ToDoListBot.Model;
using ToDoListManagement.Storage;


namespace MyApp;

internal class Program
{
    private static void Main(string[] args)
    {
        //var doItem1 = new ToDoItem("Сделать", "To do porgramm");
        //var doItem2 = new ToDoItem("Сделать1", "To do porgramm1");
        //var doItem3 = new ToDoItem("Сделать2", "To do porgramm2");

        //var manager = new ToDoListManager(new StorageTasks());

        //manager.Add(doItem1);
        //manager.Add(doItem2);
        //manager.Add(doItem3);

        //var list = manager.GetAllTasks();
        //var index = 0;
        //foreach (var item in list)
        //{
        //    Console.WriteLine(index + ":" + item.ShortName + ":" + item.Description + "-" + item.IsDone);
        //    index++;
        //}

        //Console.WriteLine("------------------------------------------------------------------");
        //manager.СompleteTask(2);
        //index = 0;
        //foreach (var item in list)
        //{
        //    Console.WriteLine(index + ":" + item.ShortName + ":" + item.Description + "-" + item.IsDone);
        //    index++;
        //}

        //manager.Delete(2);
        //Console.WriteLine("------------------------------------------------------------------");
        //index = 0;
        //foreach (var item in list)
        //{
        //    Console.WriteLine(index + ":" + item.ShortName + ":" + item.Description + "-" + item.IsDone);
        //    index++;
        //}

        using (ApplicationContext db = new ApplicationContext())
        {
            // создаем два объекта User
            var user1 = new User { NickName = "Tom",UserId = 1};
            var user2 = new User { NickName = "Alice", UserId = 2};

            // добавляем их в бд
            //db.Users.AddRange(user1, user2);
            //db.SaveChanges();

            db.ToDoItem.Add(new ToDoItem() {DateCreate = DateTime.Now.ToUniversalTime(), Description = "Lfdflq dasdasd",UserId = 1});
            db.ToDoItem.Add(new ToDoItem() { DateCreate = DateTime.Now.ToUniversalTime(), Description = "Lfdflq dasdasd", UserId = 1 });
            db.ToDoItem.Add(new ToDoItem() { DateCreate = DateTime.Now.ToUniversalTime(), Description = "Lfdflq dasdasd", UserId = 1 });
            db.ToDoItem.Add(new ToDoItem() { DateCreate = DateTime.Now.ToUniversalTime(), Description = "Lfdflq dasdasd", UserId = 1 });
            db.SaveChanges();

            var ttt = db.Users.Where(user => user.NickName == "Tom").ToList().First().Tasks;
        }

        StorageTasks maStorageTasks = new StorageTasks(new ApplicationContext());

        var trstList = maStorageTasks.GetTasks("Tom");
    }
}