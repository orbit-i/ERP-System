namespace OrbitERP.Domain.Entities.Reporting;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;

public class Dashboard : BaseEntity
{
    public string DashboardName { get; set; } = null!;
    public string DashboardCode { get; set; } = null!;
    public bool IsDefault { get; set; } = false;
    public bool IsSystem { get; set; } = false;
    public bool IsPublic { get; set; } = false;
    public string? LayoutConfig { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? OwnerId { get; set; }
    public Company Company { get; set; } = null!;
    public User? Owner { get; set; }
    public ICollection<DashboardWidget> Widgets { get; set; } = new List<DashboardWidget>();
}