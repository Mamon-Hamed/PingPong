using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Entities.Support;

namespace PingPong.Application.Features.Support.Update;

public sealed record UpdateSupportCommand(
    Guid Id,
    SupportType Type,
    string Message) : ICommand;
