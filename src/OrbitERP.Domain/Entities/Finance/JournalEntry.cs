namespace OrbitERP.Domain.Entities.Finance;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class JournalEntry : BaseEntity
{
    public string JournalNumber { get; set; } = null!;
    public DateOnly EntryDate { get; set; }
    public DateOnly? PostingDate { get; set; }
    public Guid FiscalYearId { get; set; }
    public Guid AccountingPeriodId { get; set; }
    public JournalEntryType EntryType { get; set; } = JournalEntryType.Manual;
    public JournalEntryStatus Status { get; set; } = JournalEntryStatus.Draft;
    public string Description { get; set; } = null!;
    public string? ReferenceType { get; set; }
    public Guid? ReferenceId { get; set; }
    public string? ReferenceNo { get; set; }
    public decimal TotalDebit { get; set; } = 0;
    public decimal TotalCredit { get; set; } = 0;
    public bool IsBalanced { get; set; } = false;
    public bool IsReversed { get; set; } = false;
    public Guid? ReversedByJEId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? BranchId { get; set; }
    public Guid? PostedBy { get; set; }
    public DateTime? PostedAt { get; set; }
    public FiscalYear FiscalYear { get; set; } = null!;
    public AccountingPeriod AccountingPeriod { get; set; } = null!;
    public JournalEntry? ReversedByJE { get; set; }
    public Company Company { get; set; } = null!;
    public Branch? Branch { get; set; }
    public User? PostedByUser { get; set; }
    public ICollection<JournalEntryLine> Lines { get; set; } = new List<JournalEntryLine>();
}