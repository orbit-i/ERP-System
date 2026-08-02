using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Reporting;

namespace OrbitERP.Persistence.Configurations.Reporting;

public class DashboardConfiguration : IEntityTypeConfiguration<Dashboard>
{
    public void Configure(EntityTypeBuilder<Dashboard> builder)
    {
        builder.ToTable("Dashboards");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(d => d.DashboardName).IsRequired().HasMaxLength(200);
        builder.Property(d => d.DashboardCode).IsRequired().HasMaxLength(50);

        builder.HasIndex(d => new { d.DashboardCode, d.CompanyId }).IsUnique();
        builder.HasIndex(d => d.CompanyId);
        builder.HasIndex(d => d.OwnerId);

        builder.HasOne(d => d.Company)
            .WithMany()
            .HasForeignKey(d => d.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.Owner)
            .WithMany()
            .HasForeignKey(d => d.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}