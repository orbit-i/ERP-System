using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Finance;

namespace OrbitERP.Persistence.Configurations.Finance;

public class AccountsReceivablePaymentConfiguration : IEntityTypeConfiguration<AccountsReceivablePayment>
{
    public void Configure(EntityTypeBuilder<AccountsReceivablePayment> builder)
    {
        builder.ToTable("AccountsReceivablePayments");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(a => a.Amount).HasColumnType("DECIMAL(18,2)");

        builder.Property(a => a.PaymentMethod)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(a => a.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(a => a.ReferenceNo).HasMaxLength(100);
        builder.Property(a => a.BankName).HasMaxLength(200);
        builder.Property(a => a.ChequeNo).HasMaxLength(100);
        builder.Property(a => a.Notes).HasMaxLength(500);

        builder.HasIndex(a => a.ARId);
        builder.HasIndex(a => a.PaymentDate);
        builder.HasIndex(a => a.CompanyId);

        builder.HasOne(a => a.AccountsReceivable)
            .WithMany(ar => ar.Payments)
            .HasForeignKey(a => a.ARId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Company)
            .WithMany()
            .HasForeignKey(a => a.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}