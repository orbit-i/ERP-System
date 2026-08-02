namespace OrbitERP.Domain.Entities.Procurement;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Inventory;

public class GoodsReturnLine : BaseEntity
{
    public Guid ReturnNoteId { get; set; }
    public Guid GRNLineId { get; set; }
    public Guid ProductId { get; set; }
    public int LineNumber { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; } = 0;
    public decimal LineTotal { get; set; } = 0;
    public string? Reason { get; set; }
    public GoodsReturnNote ReturnNote { get; set; } = null!;
    public GoodsReceivingLine GRNLine { get; set; } = null!;
    public Product Product { get; set; } = null!;
}