namespace OrbitERP.Domain.Entities.Inventory;

using OrbitERP.Domain.Common;

public class UnitOfMeasure : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public bool IsActive { get; set; } = true;
}