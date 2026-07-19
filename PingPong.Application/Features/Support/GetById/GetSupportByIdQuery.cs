using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Support.GetById;

public sealed record GetSupportByIdQuery(Guid Id) : GetByIdQuery<SupportId, SupportResponse>(Id);
