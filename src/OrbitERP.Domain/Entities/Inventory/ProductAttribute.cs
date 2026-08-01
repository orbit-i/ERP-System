namespace OrbitERP.Domain.Entities.Inventory;

using OrbitERP.Domain.Common;

public class ProductAttribute : BaseEntity
{
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; } = true;
}