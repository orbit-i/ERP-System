namespace OrbitERP.Domain.Entities.Inventory;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;

public class ProductCategory : BaseEntity
{
    public string Name { get; set; } = null!;
    public Guid? ParentId { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public Guid CompanyId { get; set; }
    public ProductCategory? Parent { get; set; }
    public ICollection<ProductCategory> SubCategories { get; set; } = new List<ProductCategory>();
    public Company Company { get; set; } = null!;
}