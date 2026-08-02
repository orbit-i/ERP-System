using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Procurement;

namespace OrbitERP.Persistence.Configurations.Procurement;

public class VendorContactConfiguration : IEntityTypeConfiguration<VendorContact>
{
    public void Configure(EntityTypeBuilder<VendorContact> builder)
    {
        builder.ToTable("VendorContacts");

        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(v => v.FullName).IsRequired().HasMaxLength(150);
        builder.Property(v => v.Designation).HasMaxLength(100);
        builder.Property(v => v.Email).HasMaxLength(200);
        builder.Property(v => v.Phone).HasMaxLength(50);
        builder.Property(v => v.Notes).HasMaxLength(500);

        builder.HasIndex(v => v.VendorId);

        builder.HasOne(v => v.Vendor)
            .WithMany(ven => ven.Contacts)
            .HasForeignKey(v => v.VendorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}