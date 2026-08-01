using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.HR;

namespace OrbitERP.Persistence.Configurations.HR;

public class JobApplicationConfiguration : IEntityTypeConfiguration<JobApplication>
{
    public void Configure(EntityTypeBuilder<JobApplication> builder)
    {
        builder.ToTable("JobApplications");

        builder.HasKey(j => j.Id);
        builder.Property(j => j.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(j => j.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(j => j.LastName).IsRequired().HasMaxLength(100);
        builder.Property(j => j.Email).IsRequired().HasMaxLength(200);
        builder.Property(j => j.Phone).HasMaxLength(50);
        builder.Property(j => j.ResumeUrl).HasMaxLength(500);
        builder.Property(j => j.CurrentSalary).HasColumnType("DECIMAL(18,2)");
        builder.Property(j => j.ExpectedSalary).HasColumnType("DECIMAL(18,2)");
        builder.Property(j => j.Notes).HasMaxLength(1000);

        builder.Property(j => j.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        // Map CreatedAt to AppliedAt per schema requirement
        builder.Property(j => j.CreatedAt).HasColumnName("AppliedAt");

        builder.HasIndex(j => j.JobPostingId);
        builder.HasIndex(j => j.Status);

        builder.HasOne(j => j.JobPosting)
            .WithMany(p => p.JobApplications)
            .HasForeignKey(j => j.JobPostingId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}