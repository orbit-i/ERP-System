using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Finance;

namespace OrbitERP.Persistence.Configurations.Finance;

public class AccountsPayableConfiguration : IEntityTypeConfiguration<AccountsPayable>
{
    public void Configure(EntityTypeBuilder<AccountsPayable> builder)
    {
        builder.ToTable("AccountsPayable");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(a => a.APNumber).IsRequired().HasMaxLength(50);
        
        builder.Property(a => a.TransactionType)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(a => a.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(a => a.OriginalAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(a => a.PaidAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(a => a.BalanceDue).HasColumnType("DECIMAL(18,2)");

        builder.Property(a => a.VendorInvoiceNo).HasMaxLength(100);
        builder.Property(a => a.ReferenceNo).HasMaxLength(100);
        builder.Property(a => a.Notes).HasMaxLength(1000);

        builder.HasIndex(a => a.APNumber).IsUnique();
        builder.HasIndex(a => a.VendorId);
        builder.HasIndex(a => a.PurchaseOrderId);
        builder.HasIndex(a => a.CompanyId);
        builder.HasIndex(a => a.Status);
        builder.HasIndex(a => a.DueDate);
        builder.HasIndex(a => a.TransactionDate);

        builder.HasOne(a => a.Vendor)
            .WithMany()
            .HasForeignKey(a => a.VendorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.PurchaseOrder)
            .WithMany()
            .HasForeignKey(a => a.PurchaseOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.GRN)
            .WithMany()
            .HasForeignKey(a => a.GRNId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Company)
            .WithMany()
            .HasForeignKey(a => a.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Branch)
            .WithMany()
            .HasForeignKey(a => a.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}