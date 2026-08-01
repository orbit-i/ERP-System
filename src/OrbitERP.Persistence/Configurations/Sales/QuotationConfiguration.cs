using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Sales;

namespace OrbitERP.Persistence.Configurations.Sales;

public class QuotationConfiguration : IEntityTypeConfiguration<Quotation>
{
    public void Configure(EntityTypeBuilder<Quotation> builder)
    {
        builder.ToTable("Quotations");

        builder.HasKey(q => q.Id);
        builder.Property(q => q.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(q => q.QuotationNo).IsRequired().HasMaxLength(50);

        builder.Property(q => q.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(q => q.SubTotal).HasColumnType("DECIMAL(18,2)");
        builder.Property(q => q.DiscountAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(q => q.TaxAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(q => q.TotalAmount).HasColumnType("DECIMAL(18,2)");

        builder.Property(q => q.Notes).HasMaxLength(1000);
        builder.Property(q => q.TermsConditions).HasMaxLength(2000);

        builder.HasIndex(q => q.QuotationNo).IsUnique();
        builder.HasIndex(q => q.CustomerId);
        builder.HasIndex(q => q.Status);

        builder.HasOne(q => q.Customer)
            .WithMany()
            .HasForeignKey(q => q.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(q => q.Lead)
            .WithMany()
            .HasForeignKey(q => q.LeadId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(q => q.Company)
            .WithMany()
            .HasForeignKey(q => q.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(q => q.Branch)
            .WithMany()
            .HasForeignKey(q => q.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}