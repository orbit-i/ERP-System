using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Sales;

namespace OrbitERP.Persistence.Configurations.Sales;

public class CRMStageConfiguration : IEntityTypeConfiguration<CRMStage>
{
    public void Configure(EntityTypeBuilder<CRMStage> builder)
    {
        builder.ToTable("CRMStages");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Probability).HasColumnType("DECIMAL(5,2)");
    }
}