using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Reporting;

namespace OrbitERP.Persistence.Configurations.Reporting;

public class KPIMetricConfiguration : IEntityTypeConfiguration<KPIMetric>
{
    public void Configure(EntityTypeBuilder<KPIMetric> builder)
    {
        builder.ToTable("KPIMetrics");

        builder.HasKey(k => k.Id);
        builder.Property(k => k.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(k => k.MetricName).IsRequired().HasMaxLength(100);
        builder.Property(k => k.MetricCode).IsRequired().HasMaxLength(50);

        builder.Property(k => k.MetricCategory)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(k => k.MetricValue).HasColumnType("DECIMAL(18,2)");
        builder.Property(k => k.PreviousValue).HasColumnType("DECIMAL(18,2)");
        builder.Property(k => k.TargetValue).HasColumnType("DECIMAL(18,2)");
        builder.Property(k => k.ChangePercent).HasColumnType("DECIMAL(8,2)");

        builder.Property(k => k.Unit).HasMaxLength(30);

        builder.Property(k => k.PeriodType)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Ignore(k => k.UpdatedAt);

        builder.HasIndex(k => new { k.MetricCode, k.PeriodDate, k.CompanyId }).IsUnique();
        builder.HasIndex(k => k.CompanyId);
        builder.HasIndex(k => k.MetricCode);
        builder.HasIndex(k => k.PeriodDate);
        builder.HasIndex(k => k.MetricCategory);

        builder.HasOne(k => k.Company)
            .WithMany()
            .HasForeignKey(k => k.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(k => k.Branch)
            .WithMany()
            .HasForeignKey(k => k.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}