namespace Domain.Product;


public class Product
{
  public Guid Id { get; set; }
  public string Name { get; set; } = null!;
  public string? Description { get; set; }
  public decimal Price { get; set; }
  public string? Status { get; set; }
}

