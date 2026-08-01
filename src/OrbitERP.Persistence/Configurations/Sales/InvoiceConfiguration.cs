using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Sales;

namespace OrbitERP.Persistence.Configurations.Sales;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoices");

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(i => i.InvoiceNo).IsRequired().HasMaxLength(50);

        builder.Property(i => i.InvoiceType)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(i => i.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(i => i.PaymentStatus)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(i => i.SubTotal).HasColumnType("DECIMAL(18,2)");
        builder.Property(i => i.DiscountAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(i => i.TaxAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(i => i.ShippingCost).HasColumnType("DECIMAL(18,2)");
        builder.Property(i => i.TotalAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(i => i.PaidAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(i => i.BalanceDue).HasColumnType("DECIMAL(18,2)");

        builder.Property(i => i.PaymentTerms).HasMaxLength(100);
        
        builder.Property(i => i.PaymentMethod)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(i => i.BillingAddress).HasMaxLength(500);
        builder.Property(i => i.ShippingAddress).HasMaxLength(500);
        builder.Property(i => i.Notes).HasMaxLength(1000);
        builder.Property(i => i.TermsConditions).HasMaxLength(2000);
        builder.Property(i => i.ReferenceNo).HasMaxLength(100);

        builder.HasIndex(i => i.InvoiceNo).IsUnique();
        builder.HasIndex(i => i.CustomerId);
        builder.HasIndex(i => i.SalesOrderId);
        builder.HasIndex(i => i.CompanyId);
        builder.HasIndex(i => i.BranchId);
        builder.HasIndex(i => i.Status);
        builder.HasIndex(i => i.PaymentStatus);
        builder.HasIndex(i => i.InvoiceDate);
        builder.HasIndex(i => i.DueDate);

        builder.HasOne(i => i.Customer)
            .WithMany()
            .HasForeignKey(i => i.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.SalesOrder)
            .WithMany()
            .HasForeignKey(i => i.SalesOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.Company)
            .WithMany()
            .HasForeignKey(i => i.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.Branch)
            .WithMany()
            .HasForeignKey(i => i.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}