namespace OrbitERP.Domain.Entities.Administration;

using OrbitERP.Domain.Common;

public class Company : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? LegalName { get; set; }
    public string? RegistrationNo { get; set; }
    public string? TaxNumber { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Website { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? LogoUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Branch> Branches { get; set; } = new List<Branch>();
    public User? CreatedByUser { get; set; }
}