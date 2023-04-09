using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using ToDoListBot.Model;

namespace ToDoListBot.DAL
{
    public class ApplicationContext:DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<ToDoItem> ToDoItem => Set<ToDoItem>();

        private readonly string? connectionString;
        public ApplicationContext()
        {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory());

            builder.AddJsonFile("appsettings.json");

            var config = builder.Build();

            connectionString = config.GetConnectionString("DefaultConnection");

            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ToDoList;Username=postgres;Password=1234");
            optionsBuilder.LogTo(Console.WriteLine);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<ToDoItem>().HasKey(u => u.ToDoItemId);
            //modelBuilder.ApplyConfiguration(new EntryToDoListConfiguration());
        }
    }
    //public class EntryToDoListConfiguration : IEntityTypeConfiguration<EntryToDoList>
    //{
    //    public void Configure(EntityTypeBuilder<EntryToDoList> builder)
    //    {
    //        builder.Property(p => p.UserId).HasColumnOrder(0).HasColumnName("UserID");
    //        builder.Property(p => p.ToDoItemId).HasColumnOrder(1);
    //    }
    //}
}