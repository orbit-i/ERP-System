using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Procurement;

namespace OrbitERP.Persistence.Configurations.Procurement;

public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
{
    public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
    {
        builder.ToTable("PurchaseOrders");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(p => p.PONumber).IsRequired().HasMaxLength(50);

        builder.Property(p => p.Status)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(p => p.PaymentStatus)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(p => p.SubTotal).HasColumnType("DECIMAL(18,2)");
        builder.Property(p => p.DiscountAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(p => p.TaxAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(p => p.ShippingCost).HasColumnType("DECIMAL(18,2)");
        builder.Property(p => p.TotalAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(p => p.PaidAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(p => p.BalanceDue).HasColumnType("DECIMAL(18,2)");

        builder.Property(p => p.ShippingAddress).HasMaxLength(500);
        builder.Property(p => p.PaymentTerms).HasMaxLength(100);
        builder.Property(p => p.Notes).HasMaxLength(1000);
        builder.Property(p => p.TermsConditions).HasMaxLength(2000);
        builder.Property(p => p.ReferenceNo).HasMaxLength(100);

        builder.HasIndex(p => p.PONumber).IsUnique();
        builder.HasIndex(p => p.VendorId);
        builder.HasIndex(p => p.CompanyId);
        builder.HasIndex(p => p.BranchId);
        builder.HasIndex(p => p.Status);
        builder.HasIndex(p => p.OrderDate);
        builder.HasIndex(p => p.PaymentStatus);

        builder.HasOne(p => p.Vendor)
            .WithMany()
            .HasForeignKey(p => p.VendorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Company)
            .WithMany()
            .HasForeignKey(p => p.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Branch)
            .WithMany()
            .HasForeignKey(p => p.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.ApprovedByUser)
            .WithMany()
            .HasForeignKey(p => p.ApprovedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}