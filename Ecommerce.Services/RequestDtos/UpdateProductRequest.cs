namespace Ecommerce.Services.RequestDtos;

public class UpdateProductRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Guid CategoryId { get; set; }
    public bool IsActive { get; set; }
}