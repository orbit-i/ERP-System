using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Finance;

namespace OrbitERP.Persistence.Configurations.Finance;

public class AccountsReceivableConfiguration : IEntityTypeConfiguration<AccountsReceivable>
{
    public void Configure(EntityTypeBuilder<AccountsReceivable> builder)
    {
        builder.ToTable("AccountsReceivable");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(a => a.ARNumber).IsRequired().HasMaxLength(50);

        builder.Property(a => a.TransactionType)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(a => a.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(a => a.OriginalAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(a => a.PaidAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(a => a.BalanceDue).HasColumnType("DECIMAL(18,2)");

        builder.Property(a => a.ReferenceNo).HasMaxLength(100);
        builder.Property(a => a.Notes).HasMaxLength(1000);

        builder.HasIndex(a => a.ARNumber).IsUnique();
        builder.HasIndex(a => a.CustomerId);
        builder.HasIndex(a => a.InvoiceId);
        builder.HasIndex(a => a.CompanyId);
        builder.HasIndex(a => a.Status);
        builder.HasIndex(a => a.DueDate);
        builder.HasIndex(a => a.TransactionDate);

        builder.HasOne(a => a.Customer)
            .WithMany()
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Invoice)
            .WithMany()
            .HasForeignKey(a => a.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.SalesOrder)
            .WithMany()
            .HasForeignKey(a => a.SalesOrderId)
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