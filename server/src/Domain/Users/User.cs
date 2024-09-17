namespace Domain.Users;

using Domain.Role;
using Domain.Store;

public class User
{
  public Guid Id { get; set; }
  public string FullName { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string? Phone { get; set; }
  public string? Status { get; set; }
  public Guid RoleId { get; set; }

  public Role Role { get; set; } = null!;

  public ICollection<Store> Stores { get; set; } = [];
}