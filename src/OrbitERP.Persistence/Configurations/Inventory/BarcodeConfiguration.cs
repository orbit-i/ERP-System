using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Inventory;

namespace OrbitERP.Persistence.Configurations.Inventory;

public class BarcodeConfiguration : IEntityTypeConfiguration<Barcode>
{
    public void Configure(EntityTypeBuilder<Barcode> builder)
    {
        builder.ToTable("Barcodes");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(b => b.BarcodeValue).IsRequired().HasMaxLength(200);

        builder.Property(b => b.BarcodeType)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasIndex(b => b.BarcodeValue).IsUnique();
        builder.HasIndex(b => b.ProductId);

        builder.HasOne(b => b.Product)
            .WithMany()
            .HasForeignKey(b => b.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Variant)
            .WithMany()
            .HasForeignKey(b => b.VariantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.CreatedByUser)
            .WithMany()
            .HasForeignKey(b => b.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}