namespace OrbitERP.Domain.Entities.Reporting;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;

public class UserDashboardPreference : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid DashboardId { get; set; }
    public bool IsFavorite { get; set; } = false;
    public bool IsDefault { get; set; } = false;
    public string? CustomLayout { get; set; }
    public User User { get; set; } = null!;
    public Dashboard Dashboard { get; set; } = null!;
}