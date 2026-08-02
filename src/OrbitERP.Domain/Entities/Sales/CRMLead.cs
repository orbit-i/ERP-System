namespace OrbitERP.Domain.Entities.Sales;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class CRMLead : BaseEntity
{
    public string Title { get; set; } = null!;
    public Guid? CustomerId { get; set; }
    public string? ContactName { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public Guid StageId { get; set; }
    public decimal ExpectedValue { get; set; } = 0;
    public DateOnly? ExpectedCloseDate { get; set; }
    public Guid? AssignedTo { get; set; }
    public CRMPriority Priority { get; set; } = CRMPriority.Medium;
    public CRMStatus Status { get; set; } = CRMStatus.Open;
    public string? LostReason { get; set; }
    public Guid CompanyId { get; set; }
    public Customer? Customer { get; set; }
    public CRMStage Stage { get; set; } = null!;
    public User? AssignedToUser { get; set; }
    public Company Company { get; set; } = null!;
}