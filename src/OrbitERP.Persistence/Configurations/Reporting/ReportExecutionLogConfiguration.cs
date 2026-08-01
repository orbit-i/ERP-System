using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Reporting;

namespace OrbitERP.Persistence.Configurations.Reporting;

public class ReportExecutionLogConfiguration : IEntityTypeConfiguration<ReportExecutionLog>
{
    public void Configure(EntityTypeBuilder<ReportExecutionLog> builder)
    {
        builder.ToTable("ReportExecutionLogs");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(r => r.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(r => r.ExportFormat)
            .HasConversion<string>()
            .HasMaxLength(10);

        builder.Property(r => r.FilePath).HasMaxLength(500);
        builder.Property(r => r.ErrorMessage).HasMaxLength(1000);

        builder.Ignore(r => r.UpdatedAt);

        builder.HasIndex(r => r.ReportTemplateId);
        builder.HasIndex(r => r.CompanyId);
        builder.HasIndex(r => r.ExecutedAt);
        builder.HasIndex(r => r.Status);

        builder.HasOne(r => r.ReportTemplate)
            .WithMany(t => t.ExecutionLogs)
            .HasForeignKey(r => r.ReportTemplateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.ExecutedByUser)
            .WithMany()
            .HasForeignKey(r => r.ExecutedBy)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.ReportSchedule)
            .WithMany()
            .HasForeignKey(r => r.ReportScheduleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Company)
            .WithMany()
            .HasForeignKey(r => r.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}