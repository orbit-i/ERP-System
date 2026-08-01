namespace OrbitERP.Domain.Entities.AI;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class AIPrediction : BaseEntity
{
    public AIPredictionType PredictionType { get; set; }
    public string? EntityType { get; set; }
    public Guid? EntityId { get; set; }
    public DateOnly PredictionDate { get; set; }
    public decimal PredictedValue { get; set; }
    public decimal? ActualValue { get; set; }
    public decimal? Accuracy { get; set; }
    public decimal? Confidence { get; set; }
    public string AIModel { get; set; } = "gpt-4";
    public string? ModelVersion { get; set; }
    public string? InputData { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; } = null!;
}