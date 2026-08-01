using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.HR;

namespace OrbitERP.Persistence.Configurations.HR;

public class PerformanceCriteriaConfiguration : IEntityTypeConfiguration<PerformanceCriteria>
{
    public void Configure(EntityTypeBuilder<PerformanceCriteria> builder)
    {
        builder.ToTable("PerformanceCriteria");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Description).HasMaxLength(500);
        
        builder.Property(p => p.MaxScore).HasColumnType("DECIMAL(5,2)");
        builder.Property(p => p.Weight).HasColumnType("DECIMAL(5,2)");

        builder.HasOne(p => p.Template)
            .WithMany(t => t.Criteria)
            .HasForeignKey(p => p.TemplateId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}