namespace OrbitERP.Domain.Entities.Inventory;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class Barcode : BaseEntity
{
    public Guid ProductId { get; set; }
    public Guid? VariantId { get; set; }
    public string BarcodeValue { get; set; } = null!;
    public BarcodeType BarcodeType { get; set; } = BarcodeType.Ean13;
    public bool IsDefault { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public Product Product { get; set; } = null!;
    public ProductVariant? Variant { get; set; }
    public User? CreatedByUser { get; set; }
}