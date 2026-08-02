using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Reporting;

namespace OrbitERP.Persistence.Configurations.Reporting;

public class ReportScheduleConfiguration : IEntityTypeConfiguration<ReportSchedule>
{
    public void Configure(EntityTypeBuilder<ReportSchedule> builder)
    {
        builder.ToTable("ReportSchedules");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(r => r.ScheduleName).IsRequired().HasMaxLength(200);

        builder.Property(r => r.Frequency)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(r => r.ExportFormat)
            .HasConversion<string>()
            .HasMaxLength(10);

        builder.Property(r => r.Recipients).HasMaxLength(2000);

        builder.HasIndex(r => r.ReportTemplateId);
        builder.HasIndex(r => r.CompanyId);
        builder.HasIndex(r => r.NextRunAt);
        builder.HasIndex(r => r.IsActive);

        builder.HasOne(r => r.ReportTemplate)
            .WithMany(t => t.Schedules)
            .HasForeignKey(r => r.ReportTemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Company)
            .WithMany()
            .HasForeignKey(r => r.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}