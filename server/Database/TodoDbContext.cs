namespace Database;

using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Models;


public class TodoDbContext : DbContext
{
  public DbSet<User> Users { get; set; }
  public DbSet<Role> Roles { get; set; }
  public DbSet<Store> Stores { get; set; }
  public DbSet<Type> Types { get; set; }

  public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.Entity<User>(e =>
    {
      e.HasKey(x => x.Id);
      e.Property(x => x.FullName).IsRequired();
      e.Property(x => x.Email).IsRequired();
      e.Property(x => x.Phone).IsRequired(false).HasMaxLength(15);
      e.Property(x => x.Status).IsRequired();
      e.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId);
    });

    builder.Entity<Role>(e =>
    {
      e.HasKey(x => x.Id);
      e.Property(x => x.Name).IsRequired();
      e.Property(x => x.Description).IsRequired(false);
      e.Property(x => x.Status).IsRequired(); // Active, Inactive
    });

    builder.Entity<Store>(e =>
    {
      e.HasKey(x => x.Id);
      e.Property(x => x.Name).IsRequired();
      e.Property(x => x.Description).IsRequired(false);
      e.Property(x => x.Location).IsRequired();
      e.Property(x => x.Rating).IsRequired().HasDefaultValue(0);
      e.Property(x => x.Status).IsRequired();
      e.Property(x => x.Metadata).IsRequired(false);
      e.Property(x => x.CreatedBy).IsRequired(false);
      e.Property(x => x.UpdatedAt).IsRequired();
      e.Property(x => x.CreatedAt).IsRequired();
      e.HasMany(x => x.Users).WithMany(x => x.Stores);
    });

    builder.Entity<Type>(e =>
    {
      e.HasKey(x => x.Id);
      e.Property(x => x.Name).IsRequired();
      e.Property(x => x.Description).IsRequired(false);
      e.Property(x => x.ImgUrl).IsRequired(false);
    });
  }

}