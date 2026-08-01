using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Administration;

namespace OrbitERP.Persistence.Configurations.Administration;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branches");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(b => b.Name).IsRequired().HasMaxLength(200);
        builder.Property(b => b.Code).HasMaxLength(50);
        builder.Property(b => b.Email).HasMaxLength(200);
        builder.Property(b => b.Phone).HasMaxLength(50);
        builder.Property(b => b.AddressLine1).HasMaxLength(200);
        builder.Property(b => b.AddressLine2).HasMaxLength(200);
        builder.Property(b => b.City).HasMaxLength(100);
        builder.Property(b => b.State).HasMaxLength(100);
        builder.Property(b => b.PostalCode).HasMaxLength(20);
        builder.Property(b => b.Country).HasMaxLength(100);

        builder.HasIndex(b => new { b.Code, b.CompanyId })
            .IsUnique();

        builder.HasOne(b => b.Company)
            .WithMany(c => c.Branches)
            .HasForeignKey(b => b.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}