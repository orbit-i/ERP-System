namespace OrbitERP.Domain.Entities.Finance;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Entities.Sales;
using OrbitERP.Domain.Enums;

public class AccountsReceivable : BaseEntity
{
    public string ARNumber { get; set; } = null!;
    public Guid CustomerId { get; set; }
    public Guid? InvoiceId { get; set; }
    public Guid? SalesOrderId { get; set; }
    public TransactionType TransactionType { get; set; } = TransactionType.Invoice;
    public DateOnly TransactionDate { get; set; }
    public DateOnly DueDate { get; set; }
    public AccountTransactionStatus Status { get; set; } = AccountTransactionStatus.Open;
    public decimal OriginalAmount { get; set; } = 0;
    public decimal PaidAmount { get; set; } = 0;
    public decimal BalanceDue { get; set; } = 0;
    public string? ReferenceNo { get; set; }
    public string? Notes { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? BranchId { get; set; }
    public Customer Customer { get; set; } = null!;
    public Invoice? Invoice { get; set; }
    public SalesOrder? SalesOrder { get; set; }
    public Company Company { get; set; } = null!;
    public Branch? Branch { get; set; }
    public ICollection<AccountsReceivablePayment> Payments { get; set; } = new List<AccountsReceivablePayment>();
}