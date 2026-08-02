using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.AI;

namespace OrbitERP.Persistence.Configurations.AI;

public class AIUsageLogConfiguration : IEntityTypeConfiguration<AIUsageLog>
{
    public void Configure(EntityTypeBuilder<AIUsageLog> builder)
    {
        builder.ToTable("AIUsageLogs");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(a => a.AIModel).IsRequired().HasMaxLength(100);

        builder.Property(a => a.FeatureType)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(a => a.EstimatedCostUSD).HasColumnType("DECIMAL(10,6)");
        builder.Property(a => a.ErrorCode).HasMaxLength(50);

        builder.Ignore(a => a.UpdatedAt);

        builder.HasIndex(a => a.UserId);
        builder.HasIndex(a => a.CompanyId);
        builder.HasIndex(a => a.CreatedAt);

        builder.HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Company)
            .WithMany()
            .HasForeignKey(a => a.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}