namespace OrbitERP.Domain.Entities.HR;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class PerformanceReview : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public Guid TemplateId { get; set; }
    public Guid CompanyId { get; set; }
    public string ReviewPeriod { get; set; } = null!;
    public DateOnly ReviewDate { get; set; }
    public Guid? ReviewedById { get; set; }
    public decimal TotalScore { get; set; } = 0;
    public decimal MaxScore { get; set; } = 0;
    public decimal ScorePercent { get; set; } = 0;
    public PerformanceRating? Rating { get; set; }
    public string? Comments { get; set; }
    public PerformanceReviewStatus Status { get; set; } = PerformanceReviewStatus.Draft;
    public Employee Employee { get; set; } = null!;
    public PerformanceTemplate Template { get; set; } = null!;
    public Company Company { get; set; } = null!;
    public User? ReviewedBy { get; set; }
    public ICollection<PerformanceReviewDetail> Details { get; set; } = new List<PerformanceReviewDetail>();
}