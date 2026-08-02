namespace OrbitERP.Domain.Entities.Inventory;

using System;
using OrbitERP.Domain.Enums;

public class StockMovementType
{
    public Guid Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public MovementDirection Direction { get; set; }
    public bool IsActive { get; set; } = true;
}