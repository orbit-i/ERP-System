namespace OrbitERP.Domain.Entities.Procurement;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class GoodsReturnNote : BaseEntity
{
    public string ReturnNumber { get; set; } = null!;
    public Guid GRNId { get; set; }
    public Guid VendorId { get; set; }
    public DateOnly ReturnDate { get; set; }
    public GoodsReturnStatus Status { get; set; } = GoodsReturnStatus.Draft;
    public GoodsReturnReason Reason { get; set; } = GoodsReturnReason.Defective;
    public decimal TotalQuantity { get; set; } = 0;
    public decimal TotalValue { get; set; } = 0;
    public string? Notes { get; set; }
    public Guid CompanyId { get; set; }
    public GoodsReceivingNote GRN { get; set; } = null!;
    public Vendor Vendor { get; set; } = null!;
    public Company Company { get; set; } = null!;
    public ICollection<GoodsReturnLine> Lines { get; set; } = new List<GoodsReturnLine>();
}