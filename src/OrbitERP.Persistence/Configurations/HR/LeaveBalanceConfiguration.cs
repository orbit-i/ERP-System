using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.HR;

namespace OrbitERP.Persistence.Configurations.HR;

public class LeaveBalanceConfiguration : IEntityTypeConfiguration<LeaveBalance>
{
    public void Configure(EntityTypeBuilder<LeaveBalance> builder)
    {
        builder.ToTable("LeaveBalances");

        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(l => l.TotalDays).HasColumnType("DECIMAL(5,1)");
        builder.Property(l => l.UsedDays).HasColumnType("DECIMAL(5,1)");
        builder.Property(l => l.RemainingDays).HasColumnType("DECIMAL(5,1)");

        builder.HasIndex(l => new { l.EmployeeId, l.LeaveTypeId, l.Year })
            .IsUnique();

        builder.HasOne(l => l.Employee)
            .WithMany(e => e.LeaveBalances)
            .HasForeignKey(l => l.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.LeaveType)
            .WithMany()
            .HasForeignKey(l => l.LeaveTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}