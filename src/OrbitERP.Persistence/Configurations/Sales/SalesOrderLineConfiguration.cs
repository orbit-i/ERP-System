using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Sales;

namespace OrbitERP.Persistence.Configurations.Sales;

public class SalesOrderLineConfiguration : IEntityTypeConfiguration<SalesOrderLine>
{
    public void Configure(EntityTypeBuilder<SalesOrderLine> builder)
    {
        builder.ToTable("SalesOrderLines");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(s => s.Description).HasMaxLength(500);

        builder.Property(s => s.OrderedQty).HasColumnType("DECIMAL(18,4)");
        builder.Property(s => s.DeliveredQty).HasColumnType("DECIMAL(18,4)");
        builder.Property(s => s.UnitPrice).HasColumnType("DECIMAL(18,4)");
        
        builder.Property(s => s.DiscountPct).HasColumnType("DECIMAL(5,2)");
        builder.Property(s => s.DiscountAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(s => s.TaxPct).HasColumnType("DECIMAL(5,2)");
        builder.Property(s => s.TaxAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(s => s.LineTotal).HasColumnType("DECIMAL(18,2)");

        builder.HasOne(s => s.Order)
            .WithMany(o => o.Lines)
            .HasForeignKey(s => s.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Product)
            .WithMany()
            .HasForeignKey(s => s.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Variant)
            .WithMany()
            .HasForeignKey(s => s.VariantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Uom)
            .WithMany()
            .HasForeignKey(s => s.UomId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}