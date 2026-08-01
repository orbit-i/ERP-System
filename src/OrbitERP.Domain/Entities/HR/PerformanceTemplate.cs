namespace OrbitERP.Domain.Entities.HR;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;

public class PerformanceTemplate : BaseEntity
{
    public Guid CompanyId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public Company Company { get; set; } = null!;
    public ICollection<PerformanceCriteria> Criteria { get; set; } = new List<PerformanceCriteria>();
}