namespace OrbitERP.Domain.Entities.Administration;

using OrbitERP.Domain.Common;

public class RefreshToken : BaseEntity
{
    public Guid UserId { get; set; }
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; } = false;
    public User User { get; set; } = null!;
}