namespace OrbitERP.Domain.Entities.Procurement;

using OrbitERP.Domain.Common;

public class VendorBankAccount : BaseEntity
{
    public Guid VendorId { get; set; }
    public string BankName { get; set; } = null!;
    public string AccountTitle { get; set; } = null!;
    public string AccountNumber { get; set; } = null!;
    public string? IBAN { get; set; }
    public string? BranchCode { get; set; }
    public string? BranchName { get; set; }
    public bool IsDefault { get; set; } = false;
    public Vendor Vendor { get; set; } = null!;
}