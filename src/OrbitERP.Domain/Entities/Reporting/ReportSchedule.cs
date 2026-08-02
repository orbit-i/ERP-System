namespace OrbitERP.Domain.Entities.Reporting;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class ReportSchedule : BaseEntity
{
    public Guid ReportTemplateId { get; set; }
    public string ScheduleName { get; set; } = null!;
    public ScheduleFrequency Frequency { get; set; } = ScheduleFrequency.Daily;
    public int? DayOfWeek { get; set; }
    public int? DayOfMonth { get; set; }
    public TimeOnly RunTime { get; set; } = new TimeOnly(8, 0, 0);
    public ExportFormat ExportFormat { get; set; } = ExportFormat.PDF;
    public string? Recipients { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? LastRunAt { get; set; }
    public DateTime? NextRunAt { get; set; }
    public Guid CompanyId { get; set; }
    public ReportTemplate ReportTemplate { get; set; } = null!;
    public Company Company { get; set; } = null!;
}