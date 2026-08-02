namespace OrbitERP.Domain.Entities.Sales;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Inventory;

public class QuotationLine : BaseEntity
{
    public Guid QuotationId { get; set; }
    public Guid ProductId { get; set; }
    public Guid? VariantId { get; set; }
    public string? Description { get; set; }
    public decimal Quantity { get; set; }
    public Guid UomId { get; set; }
    public decimal UnitPrice { get; set; } = 0;
    public decimal DiscountPct { get; set; } = 0;
    public decimal DiscountAmount { get; set; } = 0;
    public decimal TaxPct { get; set; } = 0;
    public decimal TaxAmount { get; set; } = 0;
    public decimal LineTotal { get; set; } = 0;
    public int SortOrder { get; set; } = 0;
    public Quotation Quotation { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public ProductVariant? Variant { get; set; }
    public UnitOfMeasure Uom { get; set; } = null!;
}