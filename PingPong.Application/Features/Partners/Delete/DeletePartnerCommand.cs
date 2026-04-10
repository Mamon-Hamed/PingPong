using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Partners.Delete;

public sealed record DeletePartnerCommand(Guid Id) : DeleteCommand<PartnerId>(Id);
