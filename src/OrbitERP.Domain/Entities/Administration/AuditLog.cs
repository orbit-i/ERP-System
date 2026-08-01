namespace OrbitERP.Domain.Entities.Administration;

using OrbitERP.Domain.Common;

public class AuditLog : BaseEntity
{
    public Guid? UserId { get; set; }
    public Guid? CompanyId { get; set; }
    public Guid? BranchId { get; set; }
    
    public string Module { get; set; } = null!;
    public string Action { get; set; } = null!;
    public string? EntityName { get; set; }
    public string? EntityId { get; set; } // String, not Guid, per schema
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public User? User { get; set; }
    public Company? Company { get; set; }
    public Branch? Branch { get; set; }
}