namespace OrbitERP.Domain.Entities.Inventory;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Enums;

public class Product : BaseEntity
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public Guid UomId { get; set; }
    public string? Description { get; set; }
    public ProductType ProductType { get; set; } = ProductType.Storable;
    public decimal CostPrice { get; set; } = 0;
    public decimal SalePrice { get; set; } = 0;
    public decimal ReorderLevel { get; set; } = 0;
    public decimal ReorderQty { get; set; } = 0;
    public string? ImageUrl { get; set; }
    public bool IsSerialized { get; set; } = false;
    public bool IsBatchTracked { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public ProductCategory Category { get; set; } = null!;
    public UnitOfMeasure Uom { get; set; } = null!;
    public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
    public ICollection<ProductAttributeValue> AttributeValues { get; set; } = new List<ProductAttributeValue>();
}