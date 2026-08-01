namespace OrbitERP.Domain.Entities.HR;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class Goal : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public Guid CompanyId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateOnly? TargetDate { get; set; }
    public DateOnly? CompletionDate { get; set; }
    public int Progress { get; set; } = 0; // 0-100%
    public GoalStatus Status { get; set; } = GoalStatus.InProgress;
    public Employee Employee { get; set; } = null!;
    public Company Company { get; set; } = null!;
}