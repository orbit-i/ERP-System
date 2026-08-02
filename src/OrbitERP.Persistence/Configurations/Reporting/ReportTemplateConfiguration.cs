using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Reporting;

namespace OrbitERP.Persistence.Configurations.Reporting;

public class ReportTemplateConfiguration : IEntityTypeConfiguration<ReportTemplate>
{
    public void Configure(EntityTypeBuilder<ReportTemplate> builder)
    {
        builder.ToTable("ReportTemplates");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(r => r.ReportName).IsRequired().HasMaxLength(200);
        builder.Property(r => r.ReportCode).IsRequired().HasMaxLength(50);

        builder.Property(r => r.ReportCategory)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(r => r.ReportType)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(r => r.Description).HasMaxLength(500);
        builder.Property(r => r.ExportFormats).IsRequired().HasMaxLength(100);

        builder.HasIndex(r => new { r.ReportCode, r.CompanyId }).IsUnique();
        builder.HasIndex(r => r.CompanyId);
        builder.HasIndex(r => r.ReportCategory);
        builder.HasIndex(r => r.IsActive);

        builder.HasOne(r => r.Company)
            .WithMany()
            .HasForeignKey(r => r.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}