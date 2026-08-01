using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Procurement;

namespace OrbitERP.Persistence.Configurations.Procurement;

public class GoodsReturnNoteConfiguration : IEntityTypeConfiguration<GoodsReturnNote>
{
    public void Configure(EntityTypeBuilder<GoodsReturnNote> builder)
    {
        builder.ToTable("GoodsReturnNotes");

        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(g => g.ReturnNumber).IsRequired().HasMaxLength(50);

        builder.Property(g => g.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(g => g.Reason)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(g => g.TotalQuantity).HasColumnType("DECIMAL(18,4)");
        builder.Property(g => g.TotalValue).HasColumnType("DECIMAL(18,2)");
        builder.Property(g => g.Notes).HasMaxLength(1000);

        builder.HasIndex(g => g.ReturnNumber).IsUnique();
        builder.HasIndex(g => g.GRNId);
        builder.HasIndex(g => g.VendorId);
        builder.HasIndex(g => g.Status);

        builder.HasOne(g => g.GRN)
            .WithMany()
            .HasForeignKey(g => g.GRNId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.Vendor)
            .WithMany()
            .HasForeignKey(g => g.VendorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.Company)
            .WithMany()
            .HasForeignKey(g => g.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}