using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Finance;

namespace OrbitERP.Persistence.Configurations.Finance;

public class BudgetRevisionConfiguration : IEntityTypeConfiguration<BudgetRevision>
{
    public void Configure(EntityTypeBuilder<BudgetRevision> builder)
    {
        builder.ToTable("BudgetRevisions");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(b => b.PreviousAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(b => b.RevisedAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(b => b.Reason).IsRequired().HasMaxLength(500);

        builder.HasIndex(b => b.BudgetId);

        builder.HasOne(b => b.Budget)
            .WithMany()
            .HasForeignKey(b => b.BudgetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.ApprovedByUser)
            .WithMany()
            .HasForeignKey(b => b.ApprovedBy)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.CreatedByUser)
            .WithMany()
            .HasForeignKey(b => b.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}