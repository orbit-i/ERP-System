namespace OrbitERP.Domain.Entities.Sales;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;

public class CustomerGroup : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Discount { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public Guid CompanyId { get; set; }
    public Company Company { get; set; } = null!;
}