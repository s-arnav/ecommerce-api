namespace Ecommerce.Services.RequestDtos;

public class UpdateCategoryRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public bool IsActive { get; set; }
}