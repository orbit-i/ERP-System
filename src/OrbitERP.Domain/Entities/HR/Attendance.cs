namespace OrbitERP.Domain.Entities.HR;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class Attendance : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public Guid? ShiftId { get; set; }
    public Guid CompanyId { get; set; }
    public DateOnly AttendanceDate { get; set; }
    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    public decimal WorkedHours { get; set; }
    public decimal OvertimeHours { get; set; }
    public AttendanceStatus Status { get; set; } = AttendanceStatus.Present;
    public string? Notes { get; set; }
    public Employee Employee { get; set; } = null!;
    public AttendanceShift? Shift { get; set; }
    public Company Company { get; set; } = null!;
}