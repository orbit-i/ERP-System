namespace OrbitERP.Domain.Entities.AI;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class AIReportRequest : BaseEntity
{
    public AIReportType ReportType { get; set; }
    public Guid RequestedBy { get; set; }
    public string Prompt { get; set; } = null!;
    public string? GeneratedSQL { get; set; }
    public string? GeneratedReport { get; set; }
    public AIReportStatus Status { get; set; } = AIReportStatus.Pending;
    public string AIModel { get; set; } = "gpt-4";
    public int TokensUsed { get; set; } = 0;
    public int? ProcessingTimeMs { get; set; }
    public string? ErrorMessage { get; set; }
    public Guid CompanyId { get; set; }
    public DateTime? CompletedAt { get; set; }
    public User RequestedByUser { get; set; } = null!;
    public Company Company { get; set; } = null!;
}