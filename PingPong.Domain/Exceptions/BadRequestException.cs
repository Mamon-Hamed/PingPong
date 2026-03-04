namespace PingPong.Domain.Exceptions;

public sealed class BadRequestException : DomainException
{
    public BadRequestException(string message) : base(message) { }

    public BadRequestException(string message, Exception innerException) : base(message, innerException) { }
}
