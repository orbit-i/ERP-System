namespace OrbitERP.Domain.Entities.Procurement;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Entities.Inventory;
using OrbitERP.Domain.Enums;

public class GoodsReceivingNote : BaseEntity
{
    public string GRNNumber { get; set; } = null!;
    public Guid? PurchaseOrderId { get; set; }
    public Guid VendorId { get; set; }
    public Guid WarehouseId { get; set; }
    public DateOnly ReceivedDate { get; set; }
    public GRNStatus Status { get; set; } = GRNStatus.Draft;
    public string? DeliveryNote { get; set; }
    public string? InvoiceReference { get; set; }
    public GRNQualityStatus QualityStatus { get; set; } = GRNQualityStatus.Pending;
    public decimal TotalQuantity { get; set; } = 0;
    public decimal TotalValue { get; set; } = 0;
    public string? Notes { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? BranchId { get; set; }
    public Guid? ReceivedBy { get; set; }
    public Guid? InspectedBy { get; set; }
    public DateTime? InspectedAt { get; set; }
    public PurchaseOrder? PurchaseOrder { get; set; }
    public Vendor Vendor { get; set; } = null!;
    public Warehouse Warehouse { get; set; } = null!;
    public Company Company { get; set; } = null!;
    public Branch? Branch { get; set; }
    public User? ReceivedByUser { get; set; }
    public User? InspectedByUser { get; set; }
    public ICollection<GoodsReceivingLine> Lines { get; set; } = new List<GoodsReceivingLine>();
}