namespace OrbitERP.Domain.Entities.Finance;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class Budget : BaseEntity
{
    public string BudgetName { get; set; } = null!;
    public string BudgetCode { get; set; } = null!;
    public Guid FiscalYearId { get; set; }
    public BudgetType BudgetType { get; set; } = BudgetType.Annual;
    public BudgetStatus Status { get; set; } = BudgetStatus.Draft;
    public decimal TotalBudgetAmount { get; set; } = 0;
    public decimal TotalActualAmount { get; set; } = 0;
    public decimal TotalVariance { get; set; } = 0;
    public string? Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public Guid? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public Guid? RejectedBy { get; set; }
    public DateTime? RejectedAt { get; set; }
    public string? RejectionReason { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? BranchId { get; set; }
    public FiscalYear FiscalYear { get; set; } = null!;
    public Company Company { get; set; } = null!;
    public Branch? Branch { get; set; }
    public User? ApprovedByUser { get; set; }
    public User? RejectedByUser { get; set; }
    public ICollection<BudgetLine> Lines { get; set; } = new List<BudgetLine>();
}