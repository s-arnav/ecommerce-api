namespace Ecommerce.Services.Records;

public class ProductRecord : BaseRecord
{
    public string name { get; set; }
    public string description { get; set; }
    public decimal price { get; set; }
    public int quantity { get; set; }
    public Guid category_id { get; set; }
}