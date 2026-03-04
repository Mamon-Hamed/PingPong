namespace PingPong.Domain.Exceptions;

public sealed class NotFoundException(string entity, object id)
    : DomainException($"The {entity} with identifier '{id}' was not found.");
