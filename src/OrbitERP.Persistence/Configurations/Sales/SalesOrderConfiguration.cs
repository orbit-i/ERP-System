using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Sales;

namespace OrbitERP.Persistence.Configurations.Sales;

public class SalesOrderConfiguration : IEntityTypeConfiguration<SalesOrder>
{
    public void Configure(EntityTypeBuilder<SalesOrder> builder)
    {
        builder.ToTable("SalesOrders");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(s => s.OrderNo).IsRequired().HasMaxLength(50);

        builder.Property(s => s.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(s => s.PaymentStatus)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(s => s.ShippingAddress).HasMaxLength(500);

        builder.Property(s => s.SubTotal).HasColumnType("DECIMAL(18,2)");
        builder.Property(s => s.DiscountAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(s => s.TaxAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(s => s.ShippingCost).HasColumnType("DECIMAL(18,2)");
        builder.Property(s => s.TotalAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(s => s.PaidAmount).HasColumnType("DECIMAL(18,2)");

        builder.Property(s => s.Notes).HasMaxLength(1000);
        builder.Property(s => s.TermsConditions).HasMaxLength(2000);

        builder.HasIndex(s => s.OrderNo).IsUnique();
        builder.HasIndex(s => s.CustomerId);
        builder.HasIndex(s => s.Status);
        builder.HasIndex(s => s.OrderDate);
        builder.HasIndex(s => s.PaymentStatus);

        builder.HasOne(s => s.Customer)
            .WithMany()
            .HasForeignKey(s => s.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Quotation)
            .WithMany()
            .HasForeignKey(s => s.QuotationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Company)
            .WithMany()
            .HasForeignKey(s => s.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Branch)
            .WithMany()
            .HasForeignKey(s => s.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.SalesRep)
            .WithMany()
            .HasForeignKey(s => s.SalesRepId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Warehouse)
            .WithMany()
            .HasForeignKey(s => s.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}