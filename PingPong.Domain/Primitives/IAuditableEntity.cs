namespace PingPong.Domain.Primitives;

public interface IAuditableEntity
{
    DateTime CreatedAtUtc { get; }
    DateTime? UpdatedAtUtc { get; }
    string? CreatedBy { get; }
    string? UpdatedBy { get; }
}
