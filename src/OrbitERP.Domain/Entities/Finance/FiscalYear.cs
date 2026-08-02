namespace OrbitERP.Domain.Entities.Finance;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class FiscalYear : BaseEntity
{
    public string FiscalYearName { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public PeriodStatus Status { get; set; } = PeriodStatus.Open;
    public bool IsCurrent { get; set; } = false;
    public Guid CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public ICollection<AccountingPeriod> Periods { get; set; } = new List<AccountingPeriod>();
}