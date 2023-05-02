using ToDoListBot.DAL;
using ToDoListBot.Model;
using ToDoListManagement;
using ToDoListManagement.Storage;


namespace MyApp;

internal class Program
{
    private static void Main(string[] args)
    {

        //using (ApplicationContext db = new ApplicationContext())
        //{
        //    // создаем два объекта User
        //    var user1 = new User { NickName = "Tom",UserId = 1};
        //    var user2 = new User { NickName = "Alice", UserId = 2};

        //    // добавляем их в бд
        //    //db.Users.AddRange(user1, user2);
        //    //db.SaveChanges();

        //    db.ToDoItem.Add(new ToDoItem() {DateCreate = DateTime.Now.ToUniversalTime(), Description = "Lfdflq dasdasd",UserId = 1});
        //    db.ToDoItem.Add(new ToDoItem() { DateCreate = DateTime.Now.ToUniversalTime(), Description = "Lfdflq dasdasd", UserId = 1 });
        //    db.ToDoItem.Add(new ToDoItem() { DateCreate = DateTime.Now.ToUniversalTime(), Description = "Lfdflq dasdasd", UserId = 1 });
        //    db.ToDoItem.Add(new ToDoItem() { DateCreate = DateTime.Now.ToUniversalTime(), Description = "Lfdflq dasdasd", UserId = 1 });
        //    db.SaveChanges();

        //    var ttt = db.Users.Where(user => user.NickName == "Tom").ToList().First().Tasks;
        //}

        //StorageTasks maStorageTasks = new StorageTasks(new ApplicationContext());

        //var trstList = maStorageTasks.GetTasks("Tom");

        ToDoListManager toDoListManager = new ToDoListManager(new StorageTasks(new ApplicationContext()));

        toDoListManager.СompleteTask("pinguin007",0);

    }
}