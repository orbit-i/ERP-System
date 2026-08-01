using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Reporting;

namespace OrbitERP.Persistence.Configurations.Reporting;

public class DashboardWidgetConfiguration : IEntityTypeConfiguration<DashboardWidget>
{
    public void Configure(EntityTypeBuilder<DashboardWidget> builder)
    {
        builder.ToTable("DashboardWidgets");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(d => d.WidgetName).IsRequired().HasMaxLength(200);
        
        builder.Property(d => d.WidgetType)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(d => d.DataSource).IsRequired().HasMaxLength(100);

        builder.HasIndex(d => d.DashboardId);

        builder.HasOne(d => d.Dashboard)
            .WithMany(dash => dash.Widgets)
            .HasForeignKey(d => d.DashboardId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}