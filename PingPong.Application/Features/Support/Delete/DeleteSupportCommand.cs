using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Support.Delete;

public sealed record DeleteSupportCommand(Guid Id) : DeleteCommand<SupportId>(Id);
