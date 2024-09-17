using Application.Shared.Type;
using Domain.Users;

namespace Domain.Store;

public class Store : BaseEntity
{
  public string Name { get; set; } = null!;
  public string? Description { get; set; }
  public string? Location { get; set; }
  public decimal Rating { get; set; }
  public string? Status { get; set; }

  public ICollection<User> Users { get; set; } = [];
}