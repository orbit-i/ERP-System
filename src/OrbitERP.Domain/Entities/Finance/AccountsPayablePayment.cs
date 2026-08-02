namespace OrbitERP.Domain.Entities.Finance;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class AccountsPayablePayment : BaseEntity
{
    public Guid APId { get; set; }
    public DateOnly PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.BankTransfer;
    public string? ReferenceNo { get; set; }
    public string? BankName { get; set; }
    public string? ChequeNo { get; set; }
    public string? Notes { get; set; }
    public PaymentRecordStatus Status { get; set; } = PaymentRecordStatus.Completed;
    public Guid CompanyId { get; set; }
    public AccountsPayable AccountsPayable { get; set; } = null!;
    public Company Company { get; set; } = null!;
}