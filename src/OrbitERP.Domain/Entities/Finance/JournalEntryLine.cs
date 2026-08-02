namespace OrbitERP.Domain.Entities.Finance;

using OrbitERP.Domain.Common;

public class JournalEntryLine : BaseEntity
{
    public Guid JournalEntryId { get; set; }
    public Guid AccountId { get; set; }
    public int LineNumber { get; set; }
    public string? Description { get; set; }
    public decimal DebitAmount { get; set; } = 0;
    public decimal CreditAmount { get; set; } = 0;
    public JournalEntry JournalEntry { get; set; } = null!;
    public ChartOfAccount Account { get; set; } = null!;
}