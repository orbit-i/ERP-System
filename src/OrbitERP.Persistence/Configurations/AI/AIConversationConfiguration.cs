using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.AI;

namespace OrbitERP.Persistence.Configurations.AI;

public class AIConversationConfiguration : IEntityTypeConfiguration<AIConversation>
{
    public void Configure(EntityTypeBuilder<AIConversation> builder)
    {
        builder.ToTable("AIConversations");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(a => a.SessionId).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Title).HasMaxLength(200);
        builder.Property(a => a.AIModel).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Module).HasMaxLength(50);

        builder.HasIndex(a => a.SessionId).IsUnique();
        builder.HasIndex(a => a.UserId);
        builder.HasIndex(a => a.CompanyId);
        builder.HasIndex(a => a.CreatedAt);

        builder.HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Company)
            .WithMany()
            .HasForeignKey(a => a.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}