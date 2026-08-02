namespace OrbitERP.Domain.Entities.Sales;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Entities.Inventory;
using OrbitERP.Domain.Enums;

public class SalesOrder : BaseEntity
{
    public string OrderNo { get; set; } = null!;
    public Guid CustomerId { get; set; }
    public Guid? QuotationId { get; set; }
    public DateOnly OrderDate { get; set; }
    public DateOnly? DeliveryDate { get; set; }
    public SalesOrderStatus Status { get; set; } = SalesOrderStatus.Draft;
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;
    public string? ShippingAddress { get; set; }
    public decimal SubTotal { get; set; } = 0;
    public decimal DiscountAmount { get; set; } = 0;
    public decimal TaxAmount { get; set; } = 0;
    public decimal ShippingCost { get; set; } = 0;
    public decimal TotalAmount { get; set; } = 0;
    public decimal PaidAmount { get; set; } = 0;
    public string? Notes { get; set; }
    public string? TermsConditions { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? BranchId { get; set; }
    public Guid? SalesRepId { get; set; }
    public Guid? WarehouseId { get; set; }
    public Customer Customer { get; set; } = null!;
    public Quotation? Quotation { get; set; }
    public Company Company { get; set; } = null!;
    public Branch? Branch { get; set; }
    public User? SalesRep { get; set; }
    public Warehouse? Warehouse { get; set; }
    public ICollection<SalesOrderLine> Lines { get; set; } = new List<SalesOrderLine>();
}