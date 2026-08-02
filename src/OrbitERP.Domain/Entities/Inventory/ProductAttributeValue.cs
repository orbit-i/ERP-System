namespace OrbitERP.Domain.Entities.Inventory;

public class ProductAttributeValue
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid AttributeId { get; set; }
    public string Value { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public ProductAttribute Attribute { get; set; } = null!;
}