using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Inventory;

namespace OrbitERP.Persistence.Configurations.Inventory;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(p => p.Code).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Description).HasMaxLength(1000);
        
        builder.Property(p => p.ProductType)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(p => p.CostPrice).HasColumnType("DECIMAL(18,4)");
        builder.Property(p => p.SalePrice).HasColumnType("DECIMAL(18,4)");
        builder.Property(p => p.ReorderLevel).HasColumnType("DECIMAL(18,4)");
        builder.Property(p => p.ReorderQty).HasColumnType("DECIMAL(18,4)");
        builder.Property(p => p.ImageUrl).HasMaxLength(500);

        builder.HasIndex(p => p.Code).IsUnique();
        builder.HasIndex(p => p.CategoryId);
        builder.HasIndex(p => p.Name);
        builder.HasIndex(p => p.IsActive);

        builder.HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Uom)
            .WithMany()
            .HasForeignKey(p => p.UomId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}