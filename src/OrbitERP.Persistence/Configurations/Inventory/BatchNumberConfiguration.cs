using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Inventory;

namespace OrbitERP.Persistence.Configurations.Inventory;

public class BatchNumberConfiguration : IEntityTypeConfiguration<BatchNumber>
{
    public void Configure(EntityTypeBuilder<BatchNumber> builder)
    {
        builder.ToTable("BatchNumbers");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(b => b.BatchNo).IsRequired().HasMaxLength(100);
        builder.Property(b => b.Quantity).HasColumnType("DECIMAL(18,4)");

        builder.Property(b => b.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(b => b.Notes).HasMaxLength(500);

        builder.HasIndex(b => new { b.BatchNo, b.ProductId, b.WarehouseId }).IsUnique();
        
        builder.HasIndex(b => b.ProductId);
        builder.HasIndex(b => b.ExpiryDate);

        builder.HasOne(b => b.Product)
            .WithMany()
            .HasForeignKey(b => b.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Variant)
            .WithMany()
            .HasForeignKey(b => b.VariantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Warehouse)
            .WithMany()
            .HasForeignKey(b => b.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}