namespace OrbitERP.Domain.Entities.Inventory;

using OrbitERP.Domain.Common;

public class ProductVariant : BaseEntity
{
    public Guid ProductId { get; set; }
    public string VariantName { get; set; } = null!;
    public string SKU { get; set; } = null!;
    public decimal CostPrice { get; set; } = 0;
    public decimal SalePrice { get; set; } = 0;
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public Product Product { get; set; } = null!;
}