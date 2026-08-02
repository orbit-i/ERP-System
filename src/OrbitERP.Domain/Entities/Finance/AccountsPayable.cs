namespace OrbitERP.Domain.Entities.Finance;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Entities.Procurement;
using OrbitERP.Domain.Enums;

public class AccountsPayable : BaseEntity
{
    public string APNumber { get; set; } = null!;
    public Guid VendorId { get; set; }
    public Guid? PurchaseOrderId { get; set; }
    public Guid? GRNId { get; set; }
    public TransactionType TransactionType { get; set; } = TransactionType.Invoice;
    public DateOnly TransactionDate { get; set; }
    public DateOnly DueDate { get; set; }
    public AccountTransactionStatus Status { get; set; } = AccountTransactionStatus.Open;
    public decimal OriginalAmount { get; set; } = 0;
    public decimal PaidAmount { get; set; } = 0;
    public decimal BalanceDue { get; set; } = 0;
    public string? VendorInvoiceNo { get; set; }
    public string? ReferenceNo { get; set; }
    public string? Notes { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? BranchId { get; set; }
    public Vendor Vendor { get; set; } = null!;
    public PurchaseOrder? PurchaseOrder { get; set; }
    public GoodsReceivingNote? GRN { get; set; }
    public Company Company { get; set; } = null!;
    public Branch? Branch { get; set; }
    public ICollection<AccountsPayablePayment> Payments { get; set; } = new List<AccountsPayablePayment>();
}