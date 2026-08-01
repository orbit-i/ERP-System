using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.Procurement;

namespace OrbitERP.Persistence.Configurations.Procurement;

public class VendorBankAccountConfiguration : IEntityTypeConfiguration<VendorBankAccount>
{
    public void Configure(EntityTypeBuilder<VendorBankAccount> builder)
    {
        builder.ToTable("VendorBankAccounts");

        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(v => v.BankName).IsRequired().HasMaxLength(200);
        builder.Property(v => v.AccountTitle).IsRequired().HasMaxLength(200);
        builder.Property(v => v.AccountNumber).IsRequired().HasMaxLength(100);
        
        builder.Property(v => v.IBAN).HasMaxLength(50);
        builder.Property(v => v.BranchCode).HasMaxLength(50);
        builder.Property(v => v.BranchName).HasMaxLength(200);

        builder.HasIndex(v => v.VendorId);

        builder.HasOne(v => v.Vendor)
            .WithMany(ven => ven.BankAccounts)
            .HasForeignKey(v => v.VendorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}