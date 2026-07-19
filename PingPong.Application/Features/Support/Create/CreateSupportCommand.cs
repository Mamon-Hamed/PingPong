using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Entities.Support;

namespace PingPong.Application.Features.Support.Create;

public sealed record CreateSupportCommand(
    SupportType Type,
    string Message) : ICommand<Guid>;
