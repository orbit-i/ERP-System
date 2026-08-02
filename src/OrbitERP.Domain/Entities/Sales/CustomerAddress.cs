namespace OrbitERP.Domain.Entities.Sales;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Enums;

public class CustomerAddress : BaseEntity
{
    public Guid CustomerId { get; set; }
    public AddressType AddressType { get; set; } = AddressType.Billing;
    public string AddressLine1 { get; set; } = null!;
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public bool IsDefault { get; set; } = false;
    public Customer Customer { get; set; } = null!;
}