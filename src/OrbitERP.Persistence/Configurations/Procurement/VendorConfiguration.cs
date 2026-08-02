using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Procurement;

namespace OrbitERP.Persistence.Configurations.Procurement;

public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
{
    public void Configure(EntityTypeBuilder<Vendor> builder)
    {
        builder.ToTable("Vendors");

        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(v => v.VendorCode).IsRequired().HasMaxLength(50);
        builder.Property(v => v.CompanyName).IsRequired().HasMaxLength(200);
        
        builder.Property(v => v.VendorType)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(v => v.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(v => v.ContactPerson).HasMaxLength(150);
        builder.Property(v => v.Email).HasMaxLength(200);
        builder.Property(v => v.Phone).HasMaxLength(50);
        builder.Property(v => v.AlternatePhone).HasMaxLength(50);
        builder.Property(v => v.Website).HasMaxLength(200);
        builder.Property(v => v.TaxNumber).HasMaxLength(100);
        builder.Property(v => v.RegistrationNo).HasMaxLength(100);
        builder.Property(v => v.PaymentTerms).HasMaxLength(100);
        builder.Property(v => v.Currency).HasMaxLength(10);
        builder.Property(v => v.Address).HasMaxLength(500);
        builder.Property(v => v.City).HasMaxLength(100);
        builder.Property(v => v.State).HasMaxLength(100);
        builder.Property(v => v.Country).HasMaxLength(100);
        builder.Property(v => v.PostalCode).HasMaxLength(20);
        builder.Property(v => v.Notes).HasMaxLength(1000);

        builder.Property(v => v.CreditLimit).HasColumnType("DECIMAL(18,2)");

        builder.HasIndex(v => v.VendorCode).IsUnique();
        builder.HasIndex(v => v.CompanyId);
        builder.HasIndex(v => v.Status);
        builder.HasIndex(v => v.VendorType);

        builder.HasOne(v => v.Company)
            .WithMany()
            .HasForeignKey(v => v.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.Branch)
            .WithMany()
            .HasForeignKey(v => v.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}