namespace OrbitERP.Domain.Entities.HR;

using OrbitERP.Domain.Common;

public class EmployeeSalaryStructure : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public Guid ComponentId { get; set; }
    public decimal Value { get; set; } = 0;
    public DateOnly EffectiveFrom { get; set; }
    public DateOnly? EffectiveTo { get; set; }
    public Employee Employee { get; set; } = null!;
    public SalaryComponent Component { get; set; } = null!;
}