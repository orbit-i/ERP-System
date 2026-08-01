namespace OrbitERP.Domain.Entities.Inventory;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class StockMovement : BaseEntity
{
    public string ReferenceNo { get; set; } = null!;
    public Guid MovementTypeId { get; set; }
    public Guid? FromWarehouseId { get; set; }
    public Guid? ToWarehouseId { get; set; }
    public DateTime MovementDate { get; set; }
    public string? Notes { get; set; }
    public StockMovementStatus Status { get; set; } = StockMovementStatus.Draft;
    public Guid CompanyId { get; set; }
    public Guid? ConfirmedBy { get; set; }
    public DateTime? ConfirmedAt { get; set; }
    public StockMovementType MovementType { get; set; } = null!;
    public Warehouse? FromWarehouse { get; set; }
    public Warehouse? ToWarehouse { get; set; }
    public Company Company { get; set; } = null!;
    public User? ConfirmedByUser { get; set; }
    public ICollection<StockMovementLine> Lines { get; set; } = new List<StockMovementLine>();
}