using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Sales;

namespace OrbitERP.Persistence.Configurations.Sales;

public class QuotationLineConfiguration : IEntityTypeConfiguration<QuotationLine>
{
    public void Configure(EntityTypeBuilder<QuotationLine> builder)
    {
        builder.ToTable("QuotationLines");

        builder.HasKey(q => q.Id);
        builder.Property(q => q.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(q => q.Description).HasMaxLength(500);

        builder.Property(q => q.Quantity).HasColumnType("DECIMAL(18,4)");
        builder.Property(q => q.UnitPrice).HasColumnType("DECIMAL(18,4)");
        builder.Property(q => q.DiscountPct).HasColumnType("DECIMAL(5,2)");
        builder.Property(q => q.DiscountAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(q => q.TaxPct).HasColumnType("DECIMAL(5,2)");
        builder.Property(q => q.TaxAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(q => q.LineTotal).HasColumnType("DECIMAL(18,2)");

        builder.HasOne(q => q.Quotation)
            .WithMany(quot => quot.Lines)
            .HasForeignKey(q => q.QuotationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(q => q.Product)
            .WithMany()
            .HasForeignKey(q => q.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(q => q.Variant)
            .WithMany()
            .HasForeignKey(q => q.VariantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(q => q.Uom)
            .WithMany()
            .HasForeignKey(q => q.UomId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}