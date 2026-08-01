namespace OrbitERP.Domain.Entities.HR;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class SalaryComponent : BaseEntity
{
    public Guid CompanyId { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public ComponentType ComponentType { get; set; } = ComponentType.Earning;
    public CalculationType CalculationType { get; set; } = CalculationType.Fixed;
    public decimal DefaultValue { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public Company Company { get; set; } = null!;
}