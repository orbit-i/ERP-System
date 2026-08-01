using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrbitERP.Domain.Entities.AI;

namespace OrbitERP.Persistence.Configurations.AI;

public class AIMessageConfiguration : IEntityTypeConfiguration<AIMessage>
{
    public void Configure(EntityTypeBuilder<AIMessage> builder)
    {
        builder.ToTable("AIMessages");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(a => a.Role)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(a => a.Content).IsRequired();
        builder.Property(a => a.ErrorMessage).HasMaxLength(500);

        builder.Ignore(a => a.UpdatedAt);

        builder.HasIndex(a => a.ConversationId);
        builder.HasIndex(a => a.CreatedAt);

        builder.HasOne(a => a.Conversation)
            .WithMany(c => c.Messages)
            .HasForeignKey(a => a.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}