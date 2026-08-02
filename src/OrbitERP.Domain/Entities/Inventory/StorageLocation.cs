namespace OrbitERP.Domain.Entities.Inventory;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Enums;

public class StorageLocation : BaseEntity
{
    public Guid WarehouseId { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public LocationType LocationType { get; set; } = LocationType.Bin;
    public Guid? ParentId { get; set; }
    public bool IsActive { get; set; } = true;
    public Warehouse Warehouse { get; set; } = null!;
    public StorageLocation? Parent { get; set; }
    public ICollection<StorageLocation> SubLocations { get; set; } = new List<StorageLocation>();
}