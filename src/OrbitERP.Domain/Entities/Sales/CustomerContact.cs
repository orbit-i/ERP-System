namespace OrbitERP.Domain.Entities.Sales;

using OrbitERP.Domain.Common;

public class CustomerContact : BaseEntity
{
    public Guid CustomerId { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string? Designation { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public bool IsPrimary { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public Customer Customer { get; set; } = null!;
}