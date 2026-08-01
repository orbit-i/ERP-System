namespace OrbitERP.Domain.Entities.Procurement;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Inventory;
using OrbitERP.Domain.Enums;

public class GoodsReceivingLine : BaseEntity
{
    public Guid GRNId { get; set; }
    public Guid? PurchaseOrderLineId { get; set; }
    public Guid ProductId { get; set; }
    public Guid? StorageLocationId { get; set; }
    public int LineNumber { get; set; }
    public string? Description { get; set; }
    public decimal OrderedQuantity { get; set; } = 0;
    public decimal ReceivedQuantity { get; set; } = 0;
    public decimal AcceptedQuantity { get; set; } = 0;
    public decimal RejectedQuantity { get; set; } = 0;
    public decimal UnitPrice { get; set; } = 0;
    public decimal LineTotal { get; set; } = 0;
    public Guid? UomId { get; set; }
    public string? BatchNumber { get; set; }
    public string? SerialNumber { get; set; }
    public DateOnly? ExpiryDate { get; set; }
    public GRNQualityStatus QualityStatus { get; set; } = GRNQualityStatus.Pending;
    public string? QualityNotes { get; set; }
    public GoodsReceivingNote GRN { get; set; } = null!;
    public PurchaseOrderLine? PurchaseOrderLine { get; set; }
    public Product Product { get; set; } = null!;
    public StorageLocation? StorageLocation { get; set; }
    public UnitOfMeasure? Uom { get; set; }
}