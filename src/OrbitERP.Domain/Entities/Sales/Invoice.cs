namespace OrbitERP.Domain.Entities.Sales;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class Invoice : BaseEntity
{
    public string InvoiceNo { get; set; } = null!;
    public InvoiceType InvoiceType { get; set; } = InvoiceType.Sales;
    public DateOnly InvoiceDate { get; set; }
    public DateOnly DueDate { get; set; }
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;
    public Guid CustomerId { get; set; }
    public Guid? SalesOrderId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? BranchId { get; set; }
    public decimal SubTotal { get; set; } = 0;
    public decimal DiscountAmount { get; set; } = 0;
    public decimal TaxAmount { get; set; } = 0;
    public decimal ShippingCost { get; set; } = 0;
    public decimal TotalAmount { get; set; } = 0;
    public decimal PaidAmount { get; set; } = 0;
    public decimal BalanceDue { get; set; } = 0;
    public string? PaymentTerms { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public string? BillingAddress { get; set; }
    public string? ShippingAddress { get; set; }
    public string? Notes { get; set; }
    public string? TermsConditions { get; set; }
    public string? ReferenceNo { get; set; }
    public DateTime? SentAt { get; set; }
    public DateTime? PaidAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    public Customer Customer { get; set; } = null!;
    public SalesOrder? SalesOrder { get; set; }
    public Company Company { get; set; } = null!;
    public Branch? Branch { get; set; }
    public ICollection<InvoiceLine> Lines { get; set; } = new List<InvoiceLine>();
    public ICollection<InvoicePayment> Payments { get; set; } = new List<InvoicePayment>();
}