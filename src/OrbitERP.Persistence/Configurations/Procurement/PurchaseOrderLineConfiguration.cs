using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Procurement;

namespace OrbitERP.Persistence.Configurations.Procurement;

public class PurchaseOrderLineConfiguration : IEntityTypeConfiguration<PurchaseOrderLine>
{
    public void Configure(EntityTypeBuilder<PurchaseOrderLine> builder)
    {
        builder.ToTable("PurchaseOrderLines");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(p => p.Description).IsRequired().HasMaxLength(500);

        builder.Property(p => p.Quantity).HasColumnType("DECIMAL(18,4)");
        builder.Property(p => p.ReceivedQuantity).HasColumnType("DECIMAL(18,4)");
        builder.Property(p => p.UnitPrice).HasColumnType("DECIMAL(18,2)");
        builder.Property(p => p.DiscountPercent).HasColumnType("DECIMAL(5,2)");
        builder.Property(p => p.DiscountAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(p => p.TaxPercent).HasColumnType("DECIMAL(5,2)");
        builder.Property(p => p.TaxAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(p => p.LineTotal).HasColumnType("DECIMAL(18,2)");

        // We changed it to UomId.
        // No need for HasColumnName("UnitOfMeasure") anymore.

        builder.HasIndex(p => p.PurchaseOrderId);
        builder.HasIndex(p => p.ProductId);

        builder.HasOne(p => p.PurchaseOrder)
            .WithMany(po => po.Lines)
            .HasForeignKey(p => p.PurchaseOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Product)
            .WithMany()
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Uom)
            .WithMany()
            .HasForeignKey(p => p.UomId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}