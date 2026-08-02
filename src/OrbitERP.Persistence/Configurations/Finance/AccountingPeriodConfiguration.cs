using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Finance;

namespace OrbitERP.Persistence.Configurations.Finance;

public class AccountingPeriodConfiguration : IEntityTypeConfiguration<AccountingPeriod>
{
    public void Configure(EntityTypeBuilder<AccountingPeriod> builder)
    {
        builder.ToTable("AccountingPeriods");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(a => a.PeriodName).IsRequired().HasMaxLength(50);

        builder.Property(a => a.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasIndex(a => a.FiscalYearId);
        builder.HasIndex(a => a.CompanyId);
        builder.HasIndex(a => a.Status);

        builder.HasOne(a => a.FiscalYear)
            .WithMany(f => f.Periods)
            .HasForeignKey(a => a.FiscalYearId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Company)
            .WithMany()
            .HasForeignKey(a => a.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}