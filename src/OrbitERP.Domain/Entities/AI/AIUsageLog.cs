namespace OrbitERP.Domain.Entities.AI;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class AIUsageLog : BaseEntity
{
    public Guid UserId { get; set; }
    public string AIModel { get; set; } = null!;
    public AIFeatureType FeatureType { get; set; }
    public int TokensInput { get; set; } = 0;
    public int TokensOutput { get; set; } = 0;
    public int TotalTokens { get; set; } = 0;
    public decimal EstimatedCostUSD { get; set; } = 0;
    public int? ResponseTimeMs { get; set; }
    public bool IsSuccess { get; set; } = true;
    public string? ErrorCode { get; set; }
    public Guid CompanyId { get; set; }
    public User User { get; set; } = null!;
    public Company Company { get; set; } = null!;
}