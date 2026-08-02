namespace OrbitERP.Domain.Entities.Finance;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;

public class BudgetRevision : BaseEntity
{
    public Guid BudgetId { get; set; }
    public int RevisionNumber { get; set; }
    public DateOnly RevisionDate { get; set; }
    public decimal PreviousAmount { get; set; } = 0;
    public decimal RevisedAmount { get; set; } = 0;
    public string Reason { get; set; } = null!;
    public Guid? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public Budget Budget { get; set; } = null!;
    public User? ApprovedByUser { get; set; }
    public User? CreatedByUser { get; set; }
}