namespace OrbitERP.Domain.Entities.Procurement;

using OrbitERP.Domain.Common;

public class VendorContact : BaseEntity
{
    public Guid VendorId { get; set; }
    public string FullName { get; set; } = null!;
    public string? Designation { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public bool IsPrimary { get; set; } = false;
    public string? Notes { get; set; }
    public Vendor Vendor { get; set; } = null!;
}