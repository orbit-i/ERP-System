using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Reporting;

namespace OrbitERP.Persistence.Configurations.Reporting;

public class UserDashboardPreferenceConfiguration : IEntityTypeConfiguration<UserDashboardPreference>
{
    public void Configure(EntityTypeBuilder<UserDashboardPreference> builder)
    {
        builder.ToTable("UserDashboardPreferences");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.HasIndex(u => new { u.UserId, u.DashboardId }).IsUnique();
        builder.HasIndex(u => u.UserId);
        builder.HasIndex(u => u.DashboardId);

        builder.HasOne(u => u.User)
            .WithMany()
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.Dashboard)
            .WithMany()
            .HasForeignKey(u => u.DashboardId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}