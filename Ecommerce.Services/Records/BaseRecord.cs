namespace Ecommerce.Services.Records;

public class BaseRecord
{
    public Guid id { get; set; }
    public DateTime created_on { get; set; } = DateTime.UtcNow;
    public DateTime updated_on { get; set; } = DateTime.UtcNow;
    public bool is_active { get; set; } = true;
    public bool is_deleted { get; set; } = false;
}