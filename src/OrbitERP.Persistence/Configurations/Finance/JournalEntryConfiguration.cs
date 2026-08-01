using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Finance;

namespace OrbitERP.Persistence.Configurations.Finance;

public class JournalEntryConfiguration : IEntityTypeConfiguration<JournalEntry>
{
    public void Configure(EntityTypeBuilder<JournalEntry> builder)
    {
        builder.ToTable("JournalEntries");

        builder.HasKey(j => j.Id);
        builder.Property(j => j.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(j => j.JournalNumber).IsRequired().HasMaxLength(50);

        builder.Property(j => j.EntryType)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(j => j.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(j => j.Description).IsRequired().HasMaxLength(500);
        builder.Property(j => j.ReferenceType).HasMaxLength(50);
        builder.Property(j => j.ReferenceNo).HasMaxLength(100);

        builder.Property(j => j.TotalDebit).HasColumnType("DECIMAL(18,2)");
        builder.Property(j => j.TotalCredit).HasColumnType("DECIMAL(18,2)");

        builder.HasIndex(j => j.JournalNumber).IsUnique();
        builder.HasIndex(j => j.CompanyId);
        builder.HasIndex(j => j.FiscalYearId);
        builder.HasIndex(j => j.AccountingPeriodId);
        builder.HasIndex(j => j.EntryDate);
        builder.HasIndex(j => j.Status);
        builder.HasIndex(j => j.EntryType);
        builder.HasIndex(j => j.ReferenceId);

        builder.HasOne(j => j.FiscalYear)
            .WithMany()
            .HasForeignKey(j => j.FiscalYearId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(j => j.AccountingPeriod)
            .WithMany()
            .HasForeignKey(j => j.AccountingPeriodId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(j => j.ReversedByJE)
            .WithMany()
            .HasForeignKey(j => j.ReversedByJEId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(j => j.Company)
            .WithMany()
            .HasForeignKey(j => j.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(j => j.Branch)
            .WithMany()
            .HasForeignKey(j => j.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(j => j.PostedByUser)
            .WithMany()
            .HasForeignKey(j => j.PostedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}