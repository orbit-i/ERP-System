namespace OrbitERP.Domain.Entities.Reporting;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class ReportExecutionLog : BaseEntity
{
    public Guid ReportTemplateId { get; set; }
    public Guid? ExecutedBy { get; set; }
    public Guid? ReportScheduleId { get; set; }
    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
    public int? ExecutionTimeMs { get; set; }
    public ReportExecutionStatus Status { get; set; } = ReportExecutionStatus.Running;
    public ExportFormat? ExportFormat { get; set; }
    public int? ResultRowCount { get; set; }
    public string? FilePath { get; set; }
    public string? ErrorMessage { get; set; }
    public string? FilterParams { get; set; }
    public Guid CompanyId { get; set; }
    public ReportTemplate ReportTemplate { get; set; } = null!;
    public User? ExecutedByUser { get; set; }
    public ReportSchedule? ReportSchedule { get; set; }
    public Company Company { get; set; } = null!;
}