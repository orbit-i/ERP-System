using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.HR;

namespace OrbitERP.Persistence.Configurations.HR;

public class JobPostingConfiguration : IEntityTypeConfiguration<JobPosting>
{
    public void Configure(EntityTypeBuilder<JobPosting> builder)
    {
        builder.ToTable("JobPostings");

        builder.HasKey(j => j.Id);
        builder.Property(j => j.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(j => j.Title).IsRequired().HasMaxLength(200);
        
        builder.Property(j => j.JobType)
            .HasConversion<string>()
            .HasMaxLength(50);
            
        builder.Property(j => j.Location).HasMaxLength(200);
        
        builder.Property(j => j.SalaryMin).HasColumnType("DECIMAL(18,2)");
        builder.Property(j => j.SalaryMax).HasColumnType("DECIMAL(18,2)");

        builder.Property(j => j.Status)
            .HasConversion<string>()
            .HasMaxLength(50);
            
        builder.HasIndex(j => j.Status);
        builder.HasIndex(j => j.CompanyId);

        builder.HasOne(j => j.Company)
            .WithMany()
            .HasForeignKey(j => j.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(j => j.Branch)
            .WithMany()
            .HasForeignKey(j => j.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(j => j.Department)
            .WithMany()
            .HasForeignKey(j => j.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(j => j.Designation)
            .WithMany()
            .HasForeignKey(j => j.DesignationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}