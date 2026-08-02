using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.HR;

namespace OrbitERP.Persistence.Configurations.HR;

public class AttendanceShiftConfiguration : IEntityTypeConfiguration<AttendanceShift>
{
    public void Configure(EntityTypeBuilder<AttendanceShift> builder)
    {
        builder.ToTable("AttendanceShifts");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
        
        builder.Property(a => a.WorkingHours)
            .HasColumnType("DECIMAL(4,2)");

        builder.HasOne(a => a.Company)
            .WithMany()
            .HasForeignKey(a => a.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}