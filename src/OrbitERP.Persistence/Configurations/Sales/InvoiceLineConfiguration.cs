using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Sales;

namespace OrbitERP.Persistence.Configurations.Sales;

public class InvoiceLineConfiguration : IEntityTypeConfiguration<InvoiceLine>
{
    public void Configure(EntityTypeBuilder<InvoiceLine> builder)
    {
        builder.ToTable("InvoiceLines");

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(i => i.Description).IsRequired().HasMaxLength(500);

        builder.Property(i => i.Quantity).HasColumnType("DECIMAL(18,4)");
        builder.Property(i => i.UnitPrice).HasColumnType("DECIMAL(18,2)");
        
        builder.Property(i => i.DiscountPercent).HasColumnType("DECIMAL(5,2)");
        builder.Property(i => i.DiscountAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(i => i.TaxPercent).HasColumnType("DECIMAL(5,2)");
        builder.Property(i => i.TaxAmount).HasColumnType("DECIMAL(18,2)");
        builder.Property(i => i.LineTotal).HasColumnType("DECIMAL(18,2)");

        builder.HasIndex(i => i.InvoiceId);
        builder.HasIndex(i => i.ProductId);

        builder.HasOne(i => i.Invoice)
            .WithMany(inv => inv.Lines)
            .HasForeignKey(i => i.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(i => i.Product)
            .WithMany()
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.Ignore(i => i.UpdatedBy);
        builder.Ignore(i => i.CreatedBy);
    }
}