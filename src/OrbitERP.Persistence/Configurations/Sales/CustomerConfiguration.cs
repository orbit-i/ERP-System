using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Sales;

namespace OrbitERP.Persistence.Configurations.Sales;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(c => c.CustomerCode).IsRequired().HasMaxLength(50);
        
        builder.Property(c => c.CustomerType)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(c => c.FirstName).HasMaxLength(100);
        builder.Property(c => c.LastName).HasMaxLength(100);
        builder.Property(c => c.CompanyName).HasMaxLength(200);
        builder.Property(c => c.Email).HasMaxLength(200);
        builder.Property(c => c.Phone).HasMaxLength(50);
        builder.Property(c => c.Mobile).HasMaxLength(50);
        builder.Property(c => c.TaxNumber).HasMaxLength(100);
        
        builder.Property(c => c.CreditLimit).HasColumnType("DECIMAL(18,2)");
        builder.Property(c => c.OpeningBalance).HasColumnType("DECIMAL(18,2)");

        builder.Property(c => c.Notes).HasMaxLength(1000);

        builder.HasIndex(c => c.CustomerCode).IsUnique();
        builder.HasIndex(c => c.CompanyId);

        builder.HasOne(c => c.Group)
            .WithMany()
            .HasForeignKey(c => c.GroupId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Company)
            .WithMany()
            .HasForeignKey(c => c.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Branch)
            .WithMany()
            .HasForeignKey(c => c.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}