namespace OrbitERP.Domain.Entities.AI;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class AIInsight : BaseEntity
{
    public AIInsightType InsightType { get; set; }
    public string Title { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public string? DetailedContent { get; set; }
    public string AIModel { get; set; } = "gpt-4";
    public decimal? Confidence { get; set; }
    public AIInsightStatus Status { get; set; } = AIInsightStatus.Active;
    public AIInsightPriority Priority { get; set; } = AIInsightPriority.Medium;
    public string? Module { get; set; }
    public string? ReferenceType { get; set; }
    public Guid? ReferenceId { get; set; }
    public DateOnly? ValidFrom { get; set; }
    public DateOnly? ValidUntil { get; set; }

    public Guid CompanyId { get; set; }
    public Guid? GeneratedBy { get; set; }
    public Guid? DismissedBy { get; set; }
    public DateTime? DismissedAt { get; set; }
    public Company Company { get; set; } = null!;
    public User? GeneratedByUser { get; set; }
    public User? DismissedByUser { get; set; }
}