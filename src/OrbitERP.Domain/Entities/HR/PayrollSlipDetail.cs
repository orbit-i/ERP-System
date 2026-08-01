namespace OrbitERP.Domain.Entities.HR;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Enums;

public class PayrollSlipDetail : BaseEntity
{
    public Guid PayrollSlipId { get; set; }
    public Guid ComponentId { get; set; }
    public string ComponentName { get; set; } = null!;
    public string ComponentType { get; set; } = null!; // Or enum, mapped to NVARCHAR(20)
    public decimal Amount { get; set; } = 0;
    public PayrollSlip PayrollSlip { get; set; } = null!;
    public SalaryComponent Component { get; set; } = null!;
}