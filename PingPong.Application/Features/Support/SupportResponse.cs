using PingPong.Domain.Entities.Support;

namespace PingPong.Application.Features.Support;

public sealed record SupportResponse(
    Guid Id,
    string Name,
    string Email,
    SupportType Type,
    string Message,
    bool FromAuthenticated);
