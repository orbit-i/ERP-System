using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Procurement;

namespace OrbitERP.Persistence.Configurations.Procurement;

public class GoodsReceivingLineConfiguration : IEntityTypeConfiguration<GoodsReceivingLine>
{
    public void Configure(EntityTypeBuilder<GoodsReceivingLine> builder)
    {
        builder.ToTable("GoodsReceivingLines");

        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(g => g.Description).HasMaxLength(500);

        builder.Property(g => g.OrderedQuantity).HasColumnType("DECIMAL(18,4)");
        builder.Property(g => g.ReceivedQuantity).HasColumnType("DECIMAL(18,4)");
        builder.Property(g => g.AcceptedQuantity).HasColumnType("DECIMAL(18,4)");
        builder.Property(g => g.RejectedQuantity).HasColumnType("DECIMAL(18,4)");
        builder.Property(g => g.UnitPrice).HasColumnType("DECIMAL(18,2)");
        builder.Property(g => g.LineTotal).HasColumnType("DECIMAL(18,2)");

        // Map to UomId in SQL
        // No need for HasColumnName("UnitOfMeasure") anymore.

        builder.Property(g => g.BatchNumber).HasMaxLength(100);
        builder.Property(g => g.SerialNumber).HasMaxLength(100);

        builder.Property(g => g.QualityStatus)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(g => g.QualityNotes).HasMaxLength(500);

        builder.HasIndex(g => g.GRNId);
        builder.HasIndex(g => g.ProductId);
        builder.HasIndex(g => g.PurchaseOrderLineId);
        builder.HasIndex(g => g.StorageLocationId);

        builder.HasOne(g => g.GRN)
            .WithMany(grn => grn.Lines)
            .HasForeignKey(g => g.GRNId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(g => g.PurchaseOrderLine)
            .WithMany()
            .HasForeignKey(g => g.PurchaseOrderLineId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.Product)
            .WithMany()
            .HasForeignKey(g => g.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.StorageLocation)
            .WithMany()
            .HasForeignKey(g => g.StorageLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        // Map foreign key to UoM
        builder.HasOne(g => g.Uom)
            .WithMany()
            .HasForeignKey(g => g.UomId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}