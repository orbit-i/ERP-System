namespace OrbitERP.Domain.Entities.HR;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class PayrollSlip : BaseEntity
{
    public Guid PayrollPeriodId { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid CompanyId { get; set; }
    public decimal BasicSalary { get; set; } = 0;
    public decimal TotalEarnings { get; set; } = 0;
    public decimal TotalDeductions { get; set; } = 0;
    public decimal NetSalary { get; set; } = 0;
    public int WorkingDays { get; set; } = 0;
    public int PresentDays { get; set; } = 0;
    public int AbsentDays { get; set; } = 0;
    public decimal OvertimeHours { get; set; } = 0;
    public decimal OvertimeAmount { get; set; } = 0;
    public PayrollSlipStatus Status { get; set; } = PayrollSlipStatus.Draft;
    public DateTime? PaidAt { get; set; }
    public PayrollPeriod PayrollPeriod { get; set; } = null!;
    public Employee Employee { get; set; } = null!;
    public Company Company { get; set; } = null!;
    public ICollection<PayrollSlipDetail> Details { get; set; } = new List<PayrollSlipDetail>();
}