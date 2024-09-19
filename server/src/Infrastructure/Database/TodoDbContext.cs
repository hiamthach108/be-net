namespace Database;

using Microsoft.EntityFrameworkCore;
using Domain.Users;
using Domain.Role;
using Domain.Store;
using Application.Shared.Constant;

public class TodoDbContext : DbContext
{
  public DbSet<User> Users { get; set; }
  public DbSet<Role> Roles { get; set; }
  public DbSet<Store> Stores { get; set; }

  public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);


    builder.Entity<User>(e =>
    {
      e.HasKey(x => x.Id);
      e.Property(x => x.FullName).IsRequired().HasMaxLength(100);
      e.Property(x => x.Email).IsRequired().HasMaxLength(100);
      e.Property(x => x.Phone).IsRequired(false).HasMaxLength(15);
      e.Property(x => x.Status).IsRequired().HasMaxLength(100);
      e.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId);
    });

    builder.Entity<Role>(e =>
    {
      e.HasKey(x => x.Id);
      e.Property(x => x.Name).IsRequired().HasMaxLength(1000);
      e.Property(x => x.Description).IsRequired(false).HasMaxLength(1000);
      e.Property(x => x.Status).IsRequired().HasMaxLength(1000);

      // Create some default roles
      e.HasData(new Role { Id = new Guid(UserConst.DEFAULT_ROLE_ADMIN_ID), Name = "Admin", Description = "Admin role", Status = "Active" });
    });

    builder.Entity<Store>(e =>
    {
      e.HasKey(x => x.Id);
      e.Property(x => x.Name).IsRequired();
      e.Property(x => x.Description).IsRequired(false).HasMaxLength(1000);
      e.Property(x => x.Location).IsRequired().HasMaxLength(1000);
      e.Property(x => x.Rating).IsRequired().HasDefaultValue(0);
      e.Property(x => x.Status).IsRequired().HasMaxLength(1000);
      e.Property(x => x.Metadata).IsRequired(false).HasMaxLength(1000);
      e.HasOne(e => e.CreatedUser).WithMany().HasForeignKey(e => e.CreatedBy).OnDelete(DeleteBehavior.NoAction);
      e.HasOne(e => e.UpdatedUser).WithMany().HasForeignKey(e => e.UpdatedBy).OnDelete(DeleteBehavior.NoAction);
      e.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("now()");
      e.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("now()");
      e.HasMany(x => x.Users).WithMany(x => x.Stores);
    });
  }

}