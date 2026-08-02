namespace OrbitERP.Domain.Entities.Reporting;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class KPIMetric : BaseEntity
{
    public string MetricName { get; set; } = null!;
    public string MetricCode { get; set; } = null!;
    public MetricCategory MetricCategory { get; set; }
    public decimal MetricValue { get; set; } = 0;
    public decimal? PreviousValue { get; set; }
    public decimal? TargetValue { get; set; }
    public decimal? ChangePercent { get; set; }
    public string? Unit { get; set; }
    public MetricPeriodType PeriodType { get; set; } = MetricPeriodType.Daily;
    public DateOnly PeriodDate { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? BranchId { get; set; }
    public Company Company { get; set; } = null!;
    public Branch? Branch { get; set; }
}