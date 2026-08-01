namespace OrbitERP.Domain.Entities.AI;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;

public class AIConversation : BaseEntity
{
    public string SessionId { get; set; } = null!;
    public Guid UserId { get; set; }
    public string? Title { get; set; }
    public string AIModel { get; set; } = "gpt-4";
    public string? Module { get; set; }
    public int TotalMessages { get; set; } = 0;
    public int TotalTokensUsed { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public Guid CompanyId { get; set; }
    public DateTime? LastMessageAt { get; set; }
    public User User { get; set; } = null!;
    public Company Company { get; set; } = null!;
    public ICollection<AIMessage> Messages { get; set; } = new List<AIMessage>();
}