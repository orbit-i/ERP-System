namespace OrbitERP.Domain.Entities.Inventory;

using System;

public class StockLevel
{
    public Guid Id { get; set; }
    public Guid WarehouseId { get; set; }
    public Guid? LocationId { get; set; }
    public Guid ProductId { get; set; }
    public Guid? VariantId { get; set; }
    public decimal Quantity { get; set; } = 0;
    public decimal ReservedQty { get; set; } = 0;
    // AvailableQty is computed in DB (Quantity - ReservedQty), we shouldn't set it in code.
    public decimal AvailableQty { get; private set; } 
    public DateTime UpdatedAt { get; set; }
    public Warehouse Warehouse { get; set; } = null!;
    public StorageLocation? Location { get; set; }
    public Product Product { get; set; } = null!;
    public ProductVariant? Variant { get; set; }
}