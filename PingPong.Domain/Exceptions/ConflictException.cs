namespace PingPong.Domain.Exceptions;

public sealed class ConflictException(string entity, object id)
    : DomainException($"The {entity} with identifier '{id}' already exists.");
