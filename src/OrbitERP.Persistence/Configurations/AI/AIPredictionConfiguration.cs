using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.AI;

namespace OrbitERP.Persistence.Configurations.AI;

public class AIPredictionConfiguration : IEntityTypeConfiguration<AIPrediction>
{
    public void Configure(EntityTypeBuilder<AIPrediction> builder)
    {
        builder.ToTable("AIPredictions");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(a => a.PredictionType)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(a => a.EntityType).HasMaxLength(50);

        builder.Property(a => a.PredictedValue).HasColumnType("DECIMAL(18,2)");
        builder.Property(a => a.ActualValue).HasColumnType("DECIMAL(18,2)");
        builder.Property(a => a.Accuracy).HasColumnType("DECIMAL(5,2)");
        builder.Property(a => a.Confidence).HasColumnType("DECIMAL(5,2)");

        builder.Property(a => a.AIModel).IsRequired().HasMaxLength(100);
        builder.Property(a => a.ModelVersion).HasMaxLength(50);

        builder.HasIndex(a => a.CompanyId);
        builder.HasIndex(a => a.PredictionType);
        builder.HasIndex(a => a.PredictionDate);
        builder.HasIndex(a => a.EntityId);

        builder.HasOne(a => a.Company)
            .WithMany()
            .HasForeignKey(a => a.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}