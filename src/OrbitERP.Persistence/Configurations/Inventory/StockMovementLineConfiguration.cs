using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Inventory;

namespace OrbitERP.Persistence.Configurations.Inventory;

public class StockMovementLineConfiguration : IEntityTypeConfiguration<StockMovementLine>
{
    public void Configure(EntityTypeBuilder<StockMovementLine> builder)
    {
        builder.ToTable("StockMovementLines");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(s => s.Quantity).HasColumnType("DECIMAL(18,4)");
        builder.Property(s => s.CostPrice).HasColumnType("DECIMAL(18,4)");
        
        builder.Property(s => s.BatchNo).HasMaxLength(100);
        builder.Property(s => s.SerialNo).HasMaxLength(100);
        builder.Property(s => s.Notes).HasMaxLength(500);

        builder.HasIndex(s => s.ProductId);
        builder.HasIndex(s => s.MovementId);

        builder.HasOne(s => s.Movement)
            .WithMany(m => m.Lines)
            .HasForeignKey(s => s.MovementId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Product)
            .WithMany()
            .HasForeignKey(s => s.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Variant)
            .WithMany()
            .HasForeignKey(s => s.VariantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.FromLocation)
            .WithMany()
            .HasForeignKey(s => s.FromLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.ToLocation)
            .WithMany()
            .HasForeignKey(s => s.ToLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Uom)
            .WithMany()
            .HasForeignKey(s => s.UomId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}