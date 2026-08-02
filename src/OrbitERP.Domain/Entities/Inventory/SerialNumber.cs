namespace OrbitERP.Domain.Entities.Inventory;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Enums;

public class SerialNumber : BaseEntity
{
    public Guid ProductId { get; set; }
    public Guid? VariantId { get; set; }
    public Guid WarehouseId { get; set; }
    public string SerialNo { get; set; } = null!;
    public SerialNumberStatus Status { get; set; } = SerialNumberStatus.Available;
    public DateOnly? PurchaseDate { get; set; }
    public DateOnly? ExpiryDate { get; set; }
    public string? Notes { get; set; }
    public Product Product { get; set; } = null!;
    public ProductVariant? Variant { get; set; }
    public Warehouse Warehouse { get; set; } = null!;
}