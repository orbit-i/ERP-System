namespace OrbitERP.Domain.Entities.Sales;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Enums;

public class InvoicePayment : BaseEntity
{
    public Guid InvoiceId { get; set; }
    public DateOnly PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string? ReferenceNo { get; set; }
    public string? Notes { get; set; }
    public Invoice Invoice { get; set; } = null!;
}