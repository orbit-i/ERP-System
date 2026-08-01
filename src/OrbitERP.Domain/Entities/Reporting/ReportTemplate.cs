namespace OrbitERP.Domain.Entities.Reporting;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class ReportTemplate : BaseEntity
{
    public string ReportName { get; set; } = null!;
    public string ReportCode { get; set; } = null!;
    public ReportCategory ReportCategory { get; set; }
    public ReportTemplateType ReportType { get; set; } = ReportTemplateType.Tabular;
    public string? Description { get; set; }
    public string? ReportSQL { get; set; }
    public string? FilterConfig { get; set; }
    public string? ColumnConfig { get; set; }
    public string? ChartConfig { get; set; }
    public bool IsSystem { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public bool IsPublic { get; set; } = false;
    public string ExportFormats { get; set; } = "PDF,EXCEL";
    public Guid CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public ICollection<ReportSchedule> Schedules { get; set; } = new List<ReportSchedule>();
    public ICollection<ReportExecutionLog> ExecutionLogs { get; set; } = new List<ReportExecutionLog>();
}