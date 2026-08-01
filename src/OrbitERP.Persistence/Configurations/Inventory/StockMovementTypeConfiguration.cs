using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Inventory;

namespace OrbitERP.Persistence.Configurations.Inventory;

public class StockMovementTypeConfiguration : IEntityTypeConfiguration<StockMovementType>
{
    public void Configure(EntityTypeBuilder<StockMovementType> builder)
    {
        builder.ToTable("StockMovementTypes");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(s => s.Code).IsRequired().HasMaxLength(50);
        builder.Property(s => s.Name).IsRequired().HasMaxLength(100);

        builder.Property(s => s.Direction)
            .HasConversion<string>()
            .HasMaxLength(10);

        builder.HasIndex(s => s.Code).IsUnique();
    }
}