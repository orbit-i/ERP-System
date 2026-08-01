using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Sales;

namespace OrbitERP.Persistence.Configurations.Sales;

public class InvoicePaymentConfiguration : IEntityTypeConfiguration<InvoicePayment>
{
    public void Configure(EntityTypeBuilder<InvoicePayment> builder)
    {
        builder.ToTable("InvoicePayments");

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(i => i.Amount).HasColumnType("DECIMAL(18,2)");

        builder.Property(i => i.PaymentMethod)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(i => i.ReferenceNo).HasMaxLength(100);
        builder.Property(i => i.Notes).HasMaxLength(500);

        builder.HasIndex(i => i.InvoiceId);
        builder.HasIndex(i => i.PaymentDate);

        builder.HasOne(i => i.Invoice)
            .WithMany(inv => inv.Payments)
            .HasForeignKey(i => i.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}