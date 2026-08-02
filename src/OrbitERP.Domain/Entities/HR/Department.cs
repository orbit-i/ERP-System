namespace OrbitERP.Domain.Entities.HR;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;

public class Department : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Guid? BranchId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public Company Company { get; set; } = null!;
    public Branch? Branch { get; set; }
    public ICollection<Designation> Designations { get; set; } = new List<Designation>();
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}