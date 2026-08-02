namespace OrbitERP.Domain.Entities.Inventory;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;

public class Warehouse : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Guid? BranchId { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string? AddressLine1 { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public bool IsActive { get; set; } = true;
    public Company Company { get; set; } = null!;
    public Branch? Branch { get; set; }
}