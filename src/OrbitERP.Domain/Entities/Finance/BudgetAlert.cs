namespace OrbitERP.Domain.Entities.Finance;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class BudgetAlert : BaseEntity
{
    public Guid BudgetId { get; set; }
    public Guid? BudgetLineId { get; set; }
    public BudgetAlertType AlertType { get; set; } = BudgetAlertType.Threshold;
    public decimal ThresholdPercent { get; set; } = 80;
    public decimal CurrentPercent { get; set; } = 0;
    public bool IsTriggered { get; set; } = false;
    public DateTime? TriggeredAt { get; set; }
    public bool IsAcknowledged { get; set; } = false;
    public Guid? AcknowledgedBy { get; set; }
    public DateTime? AcknowledgedAt { get; set; }
    public Guid CompanyId { get; set; }
    public Budget Budget { get; set; } = null!;
    public BudgetLine? BudgetLine { get; set; }
    public User? AcknowledgedByUser { get; set; }
    public Company Company { get; set; } = null!;
}