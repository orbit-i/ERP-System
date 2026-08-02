using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.HR;

namespace OrbitERP.Persistence.Configurations.HR;

public class EmployeeSalaryStructureConfiguration : IEntityTypeConfiguration<EmployeeSalaryStructure>
{
    public void Configure(EntityTypeBuilder<EmployeeSalaryStructure> builder)
    {
        builder.ToTable("EmployeeSalaryStructure");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(e => e.Value).HasColumnType("DECIMAL(18,2)");

        builder.HasOne(e => e.Employee)
            .WithMany()
            .HasForeignKey(e => e.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Component)
            .WithMany()
            .HasForeignKey(e => e.ComponentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}