using PingPong.Domain.Primitives;

namespace PingPong.Domain.Entities;

public sealed record PlayerId(Guid Value) : StronglyTypedId(Value)
{
    public static PlayerId New() => new(Guid.NewGuid());
}

public sealed class Player : Entity<PlayerId>
{
    private Player() { }

    private Player(PlayerId id, string firstName, string lastName, string email) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string Email { get; private set; } = default!;

    public static Player Create(string firstName, string lastName, string email)
    {
        var player = new Player(PlayerId.New(), firstName, lastName, email);
        player.RaiseDomainEvent(new PlayerCreatedDomainEvent(player.Id));
        return player;
    }

    public void UpdateName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}

public sealed record PlayerCreatedDomainEvent(PlayerId PlayerId) : IDomainEvent;
