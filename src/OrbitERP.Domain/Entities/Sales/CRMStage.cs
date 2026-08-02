namespace OrbitERP.Domain.Entities.Sales;

public class CRMStage
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int Sequence { get; set; } = 0;
    public decimal Probability { get; set; } = 0;
    public bool IsActive { get; set; } = true;
}