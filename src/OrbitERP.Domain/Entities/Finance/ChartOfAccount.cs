namespace OrbitERP.Domain.Entities.Finance;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class ChartOfAccount : BaseEntity
{
    public string AccountCode { get; set; } = null!;
    public string AccountName { get; set; } = null!;
    public AccountType AccountType { get; set; }
    public string? AccountSubType { get; set; }
    public NormalBalance NormalBalance { get; set; } = NormalBalance.Debit;
    public Guid? ParentAccountId { get; set; }
    public int Level { get; set; } = 1;
    public bool IsHeader { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public bool IsSystemAccount { get; set; } = false;
    public decimal OpeningBalance { get; set; } = 0;
    public decimal CurrentBalance { get; set; } = 0;
    public string? Description { get; set; }
    public Guid CompanyId { get; set; }
    public ChartOfAccount? ParentAccount { get; set; }
    public Company Company { get; set; } = null!;
    public ICollection<ChartOfAccount> SubAccounts { get; set; } = new List<ChartOfAccount>();
}