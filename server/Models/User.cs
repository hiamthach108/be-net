namespace Models;

public class Role
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string Status { get; set; }
}

public class User
{
  public Guid Id { get; set; }
  public string FullName { get; set; }
  public string Email { get; set; }
  public string Phone { get; set; }
  public string Status { get; set; }
  public Guid RoleId { get; set; }

  // Navigation property
  public Role Role { get; set; }
  public List<Store> Stores { get; set; }
}



public class Store
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string Location { get; set; }
  public decimal Rating { get; set; }
  public string Status { get; set; }
  public string Metadata { get; set; }
  public string CreatedBy { get; set; }
  public string UpdatedBy { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public List<User> Users { get; set; }
}

public class Type
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string ImgUrl { get; set; }
}
