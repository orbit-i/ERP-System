namespace OrbitERP.Domain.Entities.Finance;

using OrbitERP.Domain.Common;

public class BudgetLine : BaseEntity
{
    public Guid BudgetId { get; set; }
    public Guid AccountId { get; set; }
    public Guid? AccountingPeriodId { get; set; }
    public string? LineDescription { get; set; }
    public decimal BudgetedAmount { get; set; } = 0;
    public decimal ActualAmount { get; set; } = 0;
    public decimal Variance { get; set; } = 0;
    public decimal VariancePercent { get; set; } = 0;
    public string? Notes { get; set; }
    public Budget Budget { get; set; } = null!;
    public ChartOfAccount Account { get; set; } = null!;
    public AccountingPeriod? AccountingPeriod { get; set; }
}