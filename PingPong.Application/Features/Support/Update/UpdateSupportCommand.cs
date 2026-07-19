using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Entities.Support;

namespace PingPong.Application.Features.Support.Update;

public sealed record UpdateSupportCommand(
    Guid Id,
    string Name,
    string Email,
    SupportType Type,
    string Message) : ICommand;
