using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Procurement;

namespace OrbitERP.Persistence.Configurations.Procurement;

public class GoodsReturnLineConfiguration : IEntityTypeConfiguration<GoodsReturnLine>
{
    public void Configure(EntityTypeBuilder<GoodsReturnLine> builder)
    {
        builder.ToTable("GoodsReturnLines");

        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(g => g.Quantity).HasColumnType("DECIMAL(18,4)");
        builder.Property(g => g.UnitPrice).HasColumnType("DECIMAL(18,2)");
        builder.Property(g => g.LineTotal).HasColumnType("DECIMAL(18,2)");
        builder.Property(g => g.Reason).HasMaxLength(500);

        builder.HasIndex(g => g.ReturnNoteId);
        builder.HasIndex(g => g.ProductId);

        builder.HasOne(g => g.ReturnNote)
            .WithMany(r => r.Lines)
            .HasForeignKey(g => g.ReturnNoteId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(g => g.GRNLine)
            .WithMany()
            .HasForeignKey(g => g.GRNLineId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.Product)
            .WithMany()
            .HasForeignKey(g => g.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}