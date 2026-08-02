namespace OrbitERP.Domain.Entities.Procurement;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class Vendor : BaseEntity
{
    public string VendorCode { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string? ContactPerson { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? AlternatePhone { get; set; }
    public string? Website { get; set; }
    public string? TaxNumber { get; set; }
    public string? RegistrationNo { get; set; }
    public VendorType VendorType { get; set; } = VendorType.Supplier;
    public VendorStatus Status { get; set; } = VendorStatus.Active;
    public decimal CreditLimit { get; set; } = 0;
    public string? PaymentTerms { get; set; }
    public string Currency { get; set; } = "PKR";
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; } = "Pakistan";
    public string? PostalCode { get; set; }
    public string? Notes { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? BranchId { get; set; }
    public Company Company { get; set; } = null!;
    public Branch? Branch { get; set; }
    public ICollection<VendorContact> Contacts { get; set; } = new List<VendorContact>();
    public ICollection<VendorBankAccount> BankAccounts { get; set; } = new List<VendorBankAccount>();
}