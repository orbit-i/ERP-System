using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Finance;

namespace OrbitERP.Persistence.Configurations.Finance;

public class ChartOfAccountConfiguration : IEntityTypeConfiguration<ChartOfAccount>
{
    public void Configure(EntityTypeBuilder<ChartOfAccount> builder)
    {
        builder.ToTable("ChartOfAccounts");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(c => c.AccountCode).IsRequired().HasMaxLength(20);
        builder.Property(c => c.AccountName).IsRequired().HasMaxLength(200);

        builder.Property(c => c.AccountType)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(c => c.AccountSubType).HasMaxLength(50);

        builder.Property(c => c.NormalBalance)
            .HasConversion<string>()
            .HasMaxLength(10);

        builder.Property(c => c.OpeningBalance).HasColumnType("DECIMAL(18,2)");
        builder.Property(c => c.CurrentBalance).HasColumnType("DECIMAL(18,2)");
        builder.Property(c => c.Description).HasMaxLength(500);

        builder.HasIndex(c => new { c.AccountCode, c.CompanyId }).IsUnique();
        builder.HasIndex(c => c.CompanyId);
        builder.HasIndex(c => c.AccountType);
        builder.HasIndex(c => c.ParentAccountId);
        builder.HasIndex(c => c.IsActive);

        builder.HasOne(c => c.ParentAccount)
            .WithMany(p => p.SubAccounts)
            .HasForeignKey(c => c.ParentAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Company)
            .WithMany()
            .HasForeignKey(c => c.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}