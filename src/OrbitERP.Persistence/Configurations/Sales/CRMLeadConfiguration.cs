using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Sales;

namespace OrbitERP.Persistence.Configurations.Sales;

public class CRMLeadConfiguration : IEntityTypeConfiguration<CRMLead>
{
    public void Configure(EntityTypeBuilder<CRMLead> builder)
    {
        builder.ToTable("CRMLeads");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(c => c.Title).IsRequired().HasMaxLength(200);
        builder.Property(c => c.ContactName).HasMaxLength(200);
        builder.Property(c => c.ContactEmail).HasMaxLength(200);
        builder.Property(c => c.ContactPhone).HasMaxLength(50);

        builder.Property(c => c.ExpectedValue).HasColumnType("DECIMAL(18,2)");

        builder.Property(c => c.Priority)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(c => c.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(c => c.LostReason).HasMaxLength(500);

        builder.HasIndex(c => c.StageId);
        builder.HasIndex(c => c.Status);
        builder.HasIndex(c => c.AssignedTo);

        builder.HasOne(c => c.Customer)
            .WithMany()
            .HasForeignKey(c => c.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Stage)
            .WithMany()
            .HasForeignKey(c => c.StageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.AssignedToUser)
            .WithMany()
            .HasForeignKey(c => c.AssignedTo)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Company)
            .WithMany()
            .HasForeignKey(c => c.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.Ignore(c => c.UpdatedBy);
    }
}