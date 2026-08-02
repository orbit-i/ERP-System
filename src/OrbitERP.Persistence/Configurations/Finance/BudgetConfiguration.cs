using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Finance;

namespace OrbitERP.Persistence.Configurations.Finance;

public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable("Budgets");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(b => b.BudgetName).IsRequired().HasMaxLength(200);
        builder.Property(b => b.BudgetCode).IsRequired().HasMaxLength(50);

        builder.Property(b => b.BudgetType)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(b => b.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(b => b.TotalBudgetAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(b => b.TotalActualAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(b => b.TotalVariance).HasColumnType("DECIMAL(18,2)");

        builder.Property(b => b.Description).HasMaxLength(1000);
        builder.Property(b => b.RejectionReason).HasMaxLength(500);

        builder.HasIndex(b => new { b.BudgetCode, b.CompanyId }).IsUnique();
        builder.HasIndex(b => b.FiscalYearId);
        builder.HasIndex(b => b.CompanyId);
        builder.HasIndex(b => b.BranchId);
        builder.HasIndex(b => b.Status);
        builder.HasIndex(b => b.BudgetType);

        builder.HasOne(b => b.FiscalYear)
            .WithMany()
            .HasForeignKey(b => b.FiscalYearId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Company)
            .WithMany()
            .HasForeignKey(b => b.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Branch)
            .WithMany()
            .HasForeignKey(b => b.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.ApprovedByUser)
            .WithMany()
            .HasForeignKey(b => b.ApprovedBy)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.RejectedByUser)
            .WithMany()
            .HasForeignKey(b => b.RejectedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}