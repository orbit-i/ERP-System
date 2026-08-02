namespace OrbitERP.Domain.Entities.Finance;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class AccountingPeriod : BaseEntity
{
    public Guid FiscalYearId { get; set; }
    public string PeriodName { get; set; } = null!;
    public int PeriodNumber { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public PeriodStatus Status { get; set; } = PeriodStatus.Open;
    public Guid CompanyId { get; set; }
    public FiscalYear FiscalYear { get; set; } = null!;
    public Company Company { get; set; } = null!;
}