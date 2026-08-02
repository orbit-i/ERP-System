using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Finance;

namespace OrbitERP.Persistence.Configurations.Finance;

public class BudgetAlertConfiguration : IEntityTypeConfiguration<BudgetAlert>
{
    public void Configure(EntityTypeBuilder<BudgetAlert> builder)
    {
        builder.ToTable("BudgetAlerts");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(b => b.AlertType)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(b => b.ThresholdPercent).HasColumnType("DECIMAL(5,2)");
        builder.Property(b => b.CurrentPercent).HasColumnType("DECIMAL(5,2)");

        builder.HasIndex(b => b.BudgetId);
        builder.HasIndex(b => b.CompanyId);
        builder.HasIndex(b => b.IsTriggered);

        builder.HasOne(b => b.Budget)
            .WithMany()
            .HasForeignKey(b => b.BudgetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.BudgetLine)
            .WithMany()
            .HasForeignKey(b => b.BudgetLineId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.AcknowledgedByUser)
            .WithMany()
            .HasForeignKey(b => b.AcknowledgedBy)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Company)
            .WithMany()
            .HasForeignKey(b => b.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}