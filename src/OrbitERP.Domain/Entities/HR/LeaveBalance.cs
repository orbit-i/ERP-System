namespace OrbitERP.Domain.Entities.HR;

using OrbitERP.Domain.Common;

public class LeaveBalance : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public Guid LeaveTypeId { get; set; }
    public int Year { get; set; }
    public decimal TotalDays { get; set; }
    public decimal UsedDays { get; set; }
    public decimal RemainingDays { get; set; }
    public Employee Employee { get; set; } = null!;
    public LeaveType LeaveType { get; set; } = null!;
}