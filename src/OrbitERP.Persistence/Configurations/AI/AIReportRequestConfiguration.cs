using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.AI;

namespace OrbitERP.Persistence.Configurations.AI;

public class AIReportRequestConfiguration : IEntityTypeConfiguration<AIReportRequest>
{
    public void Configure(EntityTypeBuilder<AIReportRequest> builder)
    {
        builder.ToTable("AIReportRequests");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(a => a.ReportType)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(a => a.Prompt).IsRequired().HasMaxLength(2000);

        builder.Property(a => a.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(a => a.AIModel).IsRequired().HasMaxLength(100);
        builder.Property(a => a.ErrorMessage).HasMaxLength(1000);

        builder.HasIndex(a => a.RequestedBy);
        builder.HasIndex(a => a.CompanyId);
        builder.HasIndex(a => a.Status);

        builder.HasOne(a => a.RequestedByUser)
            .WithMany()
            .HasForeignKey(a => a.RequestedBy)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Company)
            .WithMany()
            .HasForeignKey(a => a.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}