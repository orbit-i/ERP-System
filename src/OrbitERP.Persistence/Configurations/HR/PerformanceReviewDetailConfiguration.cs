using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.HR;

namespace OrbitERP.Persistence.Configurations.HR;

public class PerformanceReviewDetailConfiguration : IEntityTypeConfiguration<PerformanceReviewDetail>
{
    public void Configure(EntityTypeBuilder<PerformanceReviewDetail> builder)
    {
        builder.ToTable("PerformanceReviewDetails");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(p => p.Score).HasColumnType("DECIMAL(5,2)");
        builder.Property(p => p.Comments).HasMaxLength(500);

        builder.HasOne(p => p.Review)
            .WithMany(r => r.Details)
            .HasForeignKey(p => p.ReviewId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Criteria)
            .WithMany()
            .HasForeignKey(p => p.CriteriaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}