namespace OrbitERP.Domain.Entities.Inventory;

using System;

public class StockMovementLine
{
    public Guid Id { get; set; }
    public Guid MovementId { get; set; }
    public Guid ProductId { get; set; }
    public Guid? VariantId { get; set; }
    public Guid? FromLocationId { get; set; }
    public Guid? ToLocationId { get; set; }
    public decimal Quantity { get; set; }
    public Guid UomId { get; set; }
    public decimal CostPrice { get; set; } = 0;
    public string? BatchNo { get; set; }
    public string? SerialNo { get; set; }
    public DateOnly? ExpiryDate { get; set; }
    public string? Notes { get; set; }
    public StockMovement Movement { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public ProductVariant? Variant { get; set; }
    public StorageLocation? FromLocation { get; set; }
    public StorageLocation? ToLocation { get; set; }
    public UnitOfMeasure Uom { get; set; } = null!;
}