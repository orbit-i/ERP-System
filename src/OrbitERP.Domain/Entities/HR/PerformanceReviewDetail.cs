namespace OrbitERP.Domain.Entities.HR;

using OrbitERP.Domain.Common;

public class PerformanceReviewDetail : BaseEntity
{
    public Guid ReviewId { get; set; }
    public Guid CriteriaId { get; set; }
    public decimal Score { get; set; } = 0;
    public string? Comments { get; set; }
    public PerformanceReview Review { get; set; } = null!;
    public PerformanceCriteria Criteria { get; set; } = null!;
}