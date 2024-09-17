namespace Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class TodoDbContextFactory : IDesignTimeDbContextFactory<TodoDbContext>
{
  public TodoDbContext CreateDbContext(string[] args)
  {
    var configuration = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json")
      .Build();

    Console.WriteLine($"Using ConnectionString: {configuration.GetConnectionString("DatabaseConnection")}");

    var optionsBuilder = new DbContextOptionsBuilder<TodoDbContext>();
    optionsBuilder.UseNpgsql(configuration.GetConnectionString("DatabaseConnection") ?? "");

    return new TodoDbContext(optionsBuilder.Options);
  }
}
