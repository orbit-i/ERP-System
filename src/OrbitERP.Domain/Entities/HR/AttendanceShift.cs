namespace OrbitERP.Domain.Entities.HR;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;

public class AttendanceShift : BaseEntity
{
    public Guid CompanyId { get; set; }
    public string Name { get; set; } = null!;
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public decimal WorkingHours { get; set; } = 8;
    public bool IsNightShift { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public Company Company { get; set; } = null!;
}