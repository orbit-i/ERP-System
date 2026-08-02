namespace OrbitERP.Domain.Entities.Procurement;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class PurchaseOrder : BaseEntity
{
    public string PONumber { get; set; } = null!;
    public Guid VendorId { get; set; }
    public DateOnly OrderDate { get; set; }
    public DateOnly? ExpectedDelivery { get; set; }
    public DateOnly? ActualDelivery { get; set; }
    public PurchaseOrderStatus Status { get; set; } = PurchaseOrderStatus.Draft;
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;
    public decimal SubTotal { get; set; } = 0;
    public decimal DiscountAmount { get; set; } = 0;
    public decimal TaxAmount { get; set; } = 0;
    public decimal ShippingCost { get; set; } = 0;
    public decimal TotalAmount { get; set; } = 0;
    public decimal PaidAmount { get; set; } = 0;
    public decimal BalanceDue { get; set; } = 0;
    public string? ShippingAddress { get; set; }
    public string? PaymentTerms { get; set; }
    public string? Notes { get; set; }
    public string? TermsConditions { get; set; }
    public string? ReferenceNo { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? BranchId { get; set; }
    public Guid? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public Vendor Vendor { get; set; } = null!;
    public Company Company { get; set; } = null!;
    public Branch? Branch { get; set; }
    public User? ApprovedByUser { get; set; }
    public ICollection<PurchaseOrderLine> Lines { get; set; } = new List<PurchaseOrderLine>();
}