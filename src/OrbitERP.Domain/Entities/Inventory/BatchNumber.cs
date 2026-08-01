namespace OrbitERP.Domain.Entities.Inventory;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Enums;

public class BatchNumber : BaseEntity
{
    public Guid ProductId { get; set; }
    public Guid? VariantId { get; set; }
    public Guid WarehouseId { get; set; }
    public string BatchNo { get; set; } = null!;
    public decimal Quantity { get; set; } = 0;
    public DateOnly? ManufactureDate { get; set; }
    public DateOnly? ExpiryDate { get; set; }
    public BatchNumberStatus Status { get; set; } = BatchNumberStatus.Active;
    public string? Notes { get; set; }
    public Product Product { get; set; } = null!;
    public ProductVariant? Variant { get; set; }
    public Warehouse Warehouse { get; set; } = null!;
}