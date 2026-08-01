using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Inventory;

namespace OrbitERP.Persistence.Configurations.Inventory;

public class StockLevelConfiguration : IEntityTypeConfiguration<StockLevel>
{
    public void Configure(EntityTypeBuilder<StockLevel> builder)
    {
        builder.ToTable("StockLevels");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(s => s.Quantity).HasColumnType("DECIMAL(18,4)");
        builder.Property(s => s.ReservedQty).HasColumnType("DECIMAL(18,4)");
        
        // Computed column in DB
        builder.Property(s => s.AvailableQty)
            .HasColumnType("DECIMAL(18,4)")
            .ValueGeneratedOnAddOrUpdate()
            .HasComputedColumnSql("[Quantity] - [ReservedQty]");

        builder.HasIndex(s => new { s.WarehouseId, s.ProductId, s.VariantId, s.LocationId }).IsUnique();
        
        builder.HasIndex(s => s.ProductId);
        builder.HasIndex(s => s.WarehouseId);

        builder.HasOne(s => s.Warehouse)
            .WithMany()
            .HasForeignKey(s => s.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Location)
            .WithMany()
            .HasForeignKey(s => s.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Product)
            .WithMany()
            .HasForeignKey(s => s.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Variant)
            .WithMany()
            .HasForeignKey(s => s.VariantId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}