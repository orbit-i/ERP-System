namespace OrbitERP.Domain.Entities.AI;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Enums;

public class AIMessage : BaseEntity
{
    public Guid ConversationId { get; set; }
    public AIRole Role { get; set; } = AIRole.User;
    public string Content { get; set; } = null!;
    public int TokensUsed { get; set; } = 0;
    public int? ResponseTimeMs { get; set; }
    public bool IsError { get; set; } = false;
    public string? ErrorMessage { get; set; }
    public AIConversation Conversation { get; set; } = null!;
}