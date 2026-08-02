namespace OrbitERP.Domain.Entities.Sales;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class Quotation : BaseEntity
{
    public string QuotationNo { get; set; } = null!;
    public Guid CustomerId { get; set; }
    public Guid? LeadId { get; set; }
    public DateOnly QuotationDate { get; set; }
    public DateOnly? ExpiryDate { get; set; }
    public QuotationStatus Status { get; set; } = QuotationStatus.Draft;
    public decimal SubTotal { get; set; } = 0;
    public decimal DiscountAmount { get; set; } = 0;
    public decimal TaxAmount { get; set; } = 0;
    public decimal TotalAmount { get; set; } = 0;
    public string? Notes { get; set; }
    public string? TermsConditions { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? BranchId { get; set; }
    public Customer Customer { get; set; } = null!;
    public CRMLead? Lead { get; set; }
    public Company Company { get; set; } = null!;
    public Branch? Branch { get; set; }
    public ICollection<QuotationLine> Lines { get; set; } = new List<QuotationLine>();
}