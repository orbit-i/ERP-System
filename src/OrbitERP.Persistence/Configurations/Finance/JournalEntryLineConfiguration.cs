using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Finance;

namespace OrbitERP.Persistence.Configurations.Finance;

public class JournalEntryLineConfiguration : IEntityTypeConfiguration<JournalEntryLine>
{
    public void Configure(EntityTypeBuilder<JournalEntryLine> builder)
    {
        builder.ToTable("JournalEntryLines");

        builder.HasKey(j => j.Id);
        builder.Property(j => j.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(j => j.Description).HasMaxLength(500);
        builder.Property(j => j.DebitAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(j => j.CreditAmount).HasColumnType("DECIMAL(18,2)");

        builder.HasIndex(j => j.JournalEntryId);
        builder.HasIndex(j => j.AccountId);

        builder.HasOne(j => j.JournalEntry)
            .WithMany(je => je.Lines)
            .HasForeignKey(j => j.JournalEntryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(j => j.Account)
            .WithMany()
            .HasForeignKey(j => j.AccountId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}