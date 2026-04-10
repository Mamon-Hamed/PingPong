namespace PingPong.Domain.Primitives;

public interface IAuditableEntity
{
    DateTime CreatedAt { get; }
    DateTime? UpdatedAt { get; }
    string? CreatedBy { get; }
    string? CreatedByName { get; }
    string? UpdatedBy { get; }
    string? UpdatedByName { get; }
}
