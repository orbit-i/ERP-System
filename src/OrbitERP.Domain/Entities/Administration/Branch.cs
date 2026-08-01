namespace OrbitERP.Domain.Entities.Administration;

using OrbitERP.Domain.Common;

public class Branch : BaseEntity
{
    public Guid CompanyId { get; set; }
    public string Name { get; set; } = null!;
    public string? Code { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public bool IsHeadOffice { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public Company Company { get; set; } = null!;
}