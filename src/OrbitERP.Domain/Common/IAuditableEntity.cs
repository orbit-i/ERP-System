namespace OrbitERP.Domain.Common;

/// <summary>
/// Contract that every OrbitERP entity fulfils through <see cref="BaseEntity"/>.
/// <para>
/// Coding to this interface (rather than the concrete <see cref="BaseEntity"/>)
/// keeps the audit-stamping logic in <c>ApplicationDbContext</c> and any future
/// interceptors fully decoupled from the persistence model. A simple
/// <c>is IAuditableEntity</c> check is all that is needed to detect an auditable
/// entity at save time.
/// </para>
/// </summary>
public interface IAuditableEntity
{
    Guid Id { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
    Guid? CreatedBy { get; set; }
    Guid? UpdatedBy { get; set; }
}