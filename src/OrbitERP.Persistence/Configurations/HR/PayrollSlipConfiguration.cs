using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.HR;

namespace OrbitERP.Persistence.Configurations.HR;

public class PayrollSlipConfiguration : IEntityTypeConfiguration<PayrollSlip>
{
    public void Configure(EntityTypeBuilder<PayrollSlip> builder)
    {
        builder.ToTable("PayrollSlips");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(p => p.BasicSalary).HasColumnType("DECIMAL(18,2)");
        builder.Property(p => p.TotalEarnings).HasColumnType("DECIMAL(18,2)");
        builder.Property(p => p.TotalDeductions).HasColumnType("DECIMAL(18,2)");
        builder.Property(p => p.NetSalary).HasColumnType("DECIMAL(18,2)");
        builder.Property(p => p.OvertimeHours).HasColumnType("DECIMAL(4,2)");
        builder.Property(p => p.OvertimeAmount).HasColumnType("DECIMAL(18,2)");

        builder.Property(p => p.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasIndex(p => new { p.PayrollPeriodId, p.EmployeeId }).IsUnique();
        
        builder.HasIndex(p => p.EmployeeId);
        builder.HasIndex(p => p.PayrollPeriodId);
        builder.HasIndex(p => p.Status);
        builder.HasIndex(p => p.CompanyId);

        builder.HasOne(p => p.PayrollPeriod)
            .WithMany()
            .HasForeignKey(p => p.PayrollPeriodId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Employee)
            .WithMany()
            .HasForeignKey(p => p.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Company)
            .WithMany()
            .HasForeignKey(p => p.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}