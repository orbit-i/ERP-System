namespace OrbitERP.Domain.Entities.HR;

using OrbitERP.Domain.Common;

public class PerformanceCriteria : BaseEntity
{
    public Guid TemplateId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal MaxScore { get; set; } = 10;
    public decimal Weight { get; set; } = 1;
    public int SortOrder { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public PerformanceTemplate Template { get; set; } = null!;
}