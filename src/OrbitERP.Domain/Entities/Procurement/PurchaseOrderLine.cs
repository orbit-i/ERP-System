namespace OrbitERP.Domain.Entities.Procurement;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Inventory;

public class PurchaseOrderLine : BaseEntity
{
    public Guid PurchaseOrderId { get; set; }
    public Guid? ProductId { get; set; }
    public int LineNumber { get; set; }
    public string Description { get; set; } = null!;
    public decimal Quantity { get; set; } = 1;
    public decimal ReceivedQuantity { get; set; } = 0;
    public decimal UnitPrice { get; set; } = 0;
    public decimal DiscountPercent { get; set; } = 0;
    public decimal DiscountAmount { get; set; } = 0;
    public decimal TaxPercent { get; set; } = 0;
    public decimal TaxAmount { get; set; } = 0;
    public decimal LineTotal { get; set; } = 0;
    public Guid? UomId { get; set; }
    public PurchaseOrder PurchaseOrder { get; set; } = null!;
    public Product? Product { get; set; }
    public UnitOfMeasure? Uom { get; set; }
}