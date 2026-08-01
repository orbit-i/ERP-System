using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Procurement;

namespace OrbitERP.Persistence.Configurations.Procurement;

public class GoodsReceivingNoteConfiguration : IEntityTypeConfiguration<GoodsReceivingNote>
{
    public void Configure(EntityTypeBuilder<GoodsReceivingNote> builder)
    {
        builder.ToTable("GoodsReceivingNotes");

        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(g => g.GRNNumber).IsRequired().HasMaxLength(50);

        builder.Property(g => g.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(g => g.DeliveryNote).HasMaxLength(100);
        builder.Property(g => g.InvoiceReference).HasMaxLength(100);

        builder.Property(g => g.QualityStatus)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(g => g.TotalQuantity).HasColumnType("DECIMAL(18,4)");
        builder.Property(g => g.TotalValue).HasColumnType("DECIMAL(18,2)");
        builder.Property(g => g.Notes).HasMaxLength(1000);

        builder.HasIndex(g => g.GRNNumber).IsUnique();
        builder.HasIndex(g => g.PurchaseOrderId);
        builder.HasIndex(g => g.VendorId);
        builder.HasIndex(g => g.WarehouseId);
        builder.HasIndex(g => g.CompanyId);
        builder.HasIndex(g => g.Status);
        builder.HasIndex(g => g.ReceivedDate);

        builder.HasOne(g => g.PurchaseOrder)
            .WithMany()
            .HasForeignKey(g => g.PurchaseOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.Vendor)
            .WithMany()
            .HasForeignKey(g => g.VendorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.Warehouse)
            .WithMany()
            .HasForeignKey(g => g.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.Company)
            .WithMany()
            .HasForeignKey(g => g.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.Branch)
            .WithMany()
            .HasForeignKey(g => g.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.ReceivedByUser)
            .WithMany()
            .HasForeignKey(g => g.ReceivedBy)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.InspectedByUser)
            .WithMany()
            .HasForeignKey(g => g.InspectedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}