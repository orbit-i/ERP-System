using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Inventory;

namespace OrbitERP.Persistence.Configurations.Inventory;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("Warehouses");

        builder.HasKey(w => w.Id);
        builder.Property(w => w.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(w => w.Name).IsRequired().HasMaxLength(200);
        builder.Property(w => w.Code).IsRequired().HasMaxLength(50);
        builder.Property(w => w.AddressLine1).HasMaxLength(200);
        builder.Property(w => w.City).HasMaxLength(100);
        builder.Property(w => w.Country).HasMaxLength(100);

        builder.HasIndex(w => new { w.Code, w.CompanyId }).IsUnique();
        builder.HasIndex(w => w.CompanyId);

        builder.HasOne(w => w.Company)
            .WithMany()
            .HasForeignKey(w => w.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(w => w.Branch)
            .WithMany()
            .HasForeignKey(w => w.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.Ignore(w => w.UpdatedBy);
    }
}