namespace OrbitERP.Domain.Entities.Procurement;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class VendorPayment : BaseEntity
{
    public Guid VendorId { get; set; }
    public Guid? PurchaseOrderId { get; set; }
    public DateOnly PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.BankTransfer;
    public string? ReferenceNo { get; set; }
    public string? ChequeNo { get; set; }
    public string? BankName { get; set; }
    public string? Notes { get; set; }
    public VendorPaymentStatus Status { get; set; } = VendorPaymentStatus.Completed;
    public Guid CompanyId { get; set; }
    public Vendor Vendor { get; set; } = null!;
    public PurchaseOrder? PurchaseOrder { get; set; }
    public Company Company { get; set; } = null!;
}