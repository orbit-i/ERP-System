using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.HR;

namespace OrbitERP.Persistence.Configurations.HR;

public class PayrollSlipDetailConfiguration : IEntityTypeConfiguration<PayrollSlipDetail>
{
    public void Configure(EntityTypeBuilder<PayrollSlipDetail> builder)
    {
        builder.ToTable("PayrollSlipDetails");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(p => p.ComponentName).IsRequired().HasMaxLength(100);
        builder.Property(p => p.ComponentType).IsRequired().HasMaxLength(20);
        builder.Property(p => p.Amount).HasColumnType("DECIMAL(18,2)");

        builder.HasOne(p => p.PayrollSlip)
            .WithMany(s => s.Details)
            .HasForeignKey(p => p.PayrollSlipId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Component)
            .WithMany()
            .HasForeignKey(p => p.ComponentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}