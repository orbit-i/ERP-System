using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Procurement;

namespace OrbitERP.Persistence.Configurations.Procurement;

public class VendorPaymentConfiguration : IEntityTypeConfiguration<VendorPayment>
{
    public void Configure(EntityTypeBuilder<VendorPayment> builder)
    {
        builder.ToTable("VendorPayments");

        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(v => v.Amount).HasColumnType("DECIMAL(18,2)");

        builder.Property(v => v.PaymentMethod)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(v => v.ReferenceNo).HasMaxLength(100);
        builder.Property(v => v.ChequeNo).HasMaxLength(100);
        builder.Property(v => v.BankName).HasMaxLength(200);
        builder.Property(v => v.Notes).HasMaxLength(500);

        builder.Property(v => v.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasIndex(v => v.VendorId);
        builder.HasIndex(v => v.PurchaseOrderId);
        builder.HasIndex(v => v.CompanyId);
        builder.HasIndex(v => v.PaymentDate);

        builder.HasOne(v => v.Vendor)
            .WithMany()
            .HasForeignKey(v => v.VendorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.PurchaseOrder)
            .WithMany()
            .HasForeignKey(v => v.PurchaseOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.Company)
            .WithMany()
            .HasForeignKey(v => v.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}