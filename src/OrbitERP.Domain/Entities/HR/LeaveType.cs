namespace OrbitERP.Domain.Entities.HR;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;

public class LeaveType : BaseEntity
{
    public Guid CompanyId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int MaxDaysPerYear { get; set; } = 0;
    public bool IsPaid { get; set; } = true;
    public bool IsActive { get; set; } = true;
    public Company Company { get; set; } = null!;
}