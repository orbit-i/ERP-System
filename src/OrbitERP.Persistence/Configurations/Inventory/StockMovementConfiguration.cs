using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Inventory;

namespace OrbitERP.Persistence.Configurations.Inventory;

public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
{
    public void Configure(EntityTypeBuilder<StockMovement> builder)
    {
        builder.ToTable("StockMovements");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(s => s.ReferenceNo).IsRequired().HasMaxLength(50);
        builder.Property(s => s.Notes).HasMaxLength(500);

        builder.Property(s => s.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasIndex(s => s.ReferenceNo).IsUnique();
        builder.HasIndex(s => s.MovementDate);
        builder.HasIndex(s => s.Status);

        builder.HasOne(s => s.MovementType)
            .WithMany()
            .HasForeignKey(s => s.MovementTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.FromWarehouse)
            .WithMany()
            .HasForeignKey(s => s.FromWarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.ToWarehouse)
            .WithMany()
            .HasForeignKey(s => s.ToWarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Company)
            .WithMany()
            .HasForeignKey(s => s.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.ConfirmedByUser)
            .WithMany()
            .HasForeignKey(s => s.ConfirmedBy)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.Ignore(s => s.UpdatedBy);
    }
}