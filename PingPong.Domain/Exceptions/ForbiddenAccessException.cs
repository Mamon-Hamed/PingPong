namespace PingPong.Domain.Exceptions;

public sealed class ForbiddenAccessException(string message) : DomainException(message)
{
    public ForbiddenAccessException() : this("Access to this resource is forbidden.") { }
}
