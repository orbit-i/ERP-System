namespace OrbitERP.Domain.Entities.Reporting;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Enums;

public class DashboardWidget : BaseEntity
{
    public Guid DashboardId { get; set; }
    public string WidgetName { get; set; } = null!;
    public WidgetType WidgetType { get; set; }
    public string DataSource { get; set; } = null!;
    public int RefreshInterval { get; set; } = 300;
    public int PositionX { get; set; } = 0;
    public int PositionY { get; set; } = 0;
    public int Width { get; set; } = 4;
    public int Height { get; set; } = 3;
    public string? Config { get; set; }
    public string? FilterConfig { get; set; }
    public bool IsVisible { get; set; } = true;
    public int SortOrder { get; set; } = 0;
    public Dashboard Dashboard { get; set; } = null!;
}