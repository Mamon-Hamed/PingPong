using PingPong.Domain.Primitives;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Entities.Support;

public sealed class SupportMessageEntity : Entity<SupportId>
{
    private SupportMessageEntity(
        SupportId id,
        string name,
        string email,
        SupportType type,
        string message,
        bool fromAuthenticated)
        : base(id)
    {
        Name = name;
        Email = email;
        Type = type;
        Message = message;
        FromAuthenticated = fromAuthenticated;
    }

    private SupportMessageEntity()
    {
    }

    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public SupportType Type { get; private set; }
    public string Message { get; private set; } = string.Empty;
    public bool FromAuthenticated { get; private set; }

    public static SupportMessageEntity Create(
        string name,
        string email,
        SupportType type,
        string message,
        bool fromAuthenticated = false)
    {
        return new SupportMessageEntity(
            SupportId.New(),
            name,
            email,
            type,
            message,
            fromAuthenticated);
    }

    public void Update(
        string name,
        string email,
        SupportType type,
        string message)
    {
        Name = name;
        Email = email;
        Type = type;
        Message = message;
    }
}
