using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Finance;

namespace OrbitERP.Persistence.Configurations.Finance;

public class BudgetLineConfiguration : IEntityTypeConfiguration<BudgetLine>
{
    public void Configure(EntityTypeBuilder<BudgetLine> builder)
    {
        builder.ToTable("BudgetLines");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(b => b.LineDescription).HasMaxLength(500);

        builder.Property(b => b.BudgetedAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(b => b.ActualAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(b => b.Variance).HasColumnType("DECIMAL(18,2)");
        builder.Property(b => b.VariancePercent).HasColumnType("DECIMAL(8,2)");

        builder.Property(b => b.Notes).HasMaxLength(500);

        builder.HasIndex(b => b.BudgetId);
        builder.HasIndex(b => b.AccountId);
        builder.HasIndex(b => b.AccountingPeriodId);

        builder.HasOne(b => b.Budget)
            .WithMany(bud => bud.Lines)
            .HasForeignKey(b => b.BudgetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.Account)
            .WithMany()
            .HasForeignKey(b => b.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.AccountingPeriod)
            .WithMany()
            .HasForeignKey(b => b.AccountingPeriodId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}