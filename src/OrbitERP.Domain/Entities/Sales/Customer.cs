namespace OrbitERP.Domain.Entities.Sales;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class Customer : BaseEntity
{
    public string CustomerCode { get; set; } = null!;
    public CustomerType CustomerType { get; set; } = CustomerType.Individual;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CompanyName { get; set; }
    public Guid? GroupId { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public string? TaxNumber { get; set; }
    public decimal CreditLimit { get; set; } = 0;
    public int CreditDays { get; set; } = 0;
    public decimal OpeningBalance { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public string? Notes { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? BranchId { get; set; }
    public CustomerGroup? Group { get; set; }
    public Company Company { get; set; } = null!;
    public Branch? Branch { get; set; }
    public ICollection<CustomerAddress> Addresses { get; set; } = new List<CustomerAddress>();
    public ICollection<CustomerContact> Contacts { get; set; } = new List<CustomerContact>();
}