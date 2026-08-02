using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.AI;

namespace OrbitERP.Persistence.Configurations.AI;

public class AIInsightConfiguration : IEntityTypeConfiguration<AIInsight>
{
    public void Configure(EntityTypeBuilder<AIInsight> builder)
    {
        builder.ToTable("AIInsights");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(a => a.InsightType)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(a => a.Title).IsRequired().HasMaxLength(200);
        builder.Property(a => a.Summary).IsRequired().HasMaxLength(1000);
        builder.Property(a => a.AIModel).IsRequired().HasMaxLength(100);

        builder.Property(a => a.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(a => a.Priority)
            .HasConversion<string>()
            .HasMaxLength(10);

        builder.Property(a => a.Module).HasMaxLength(50);
        builder.Property(a => a.ReferenceType).HasMaxLength(50);
        builder.Property(a => a.Confidence).HasColumnType("DECIMAL(5,2)");

        builder.HasIndex(a => a.CompanyId);
        builder.HasIndex(a => a.InsightType);
        builder.HasIndex(a => a.Status);
        builder.HasIndex(a => a.Priority);
        builder.HasIndex(a => a.ValidUntil);

        builder.HasOne(a => a.Company)
            .WithMany()
            .HasForeignKey(a => a.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.GeneratedByUser)
            .WithMany()
            .HasForeignKey(a => a.GeneratedBy)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.DismissedByUser)
            .WithMany()
            .HasForeignKey(a => a.DismissedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}