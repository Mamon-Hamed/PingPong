using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Entities.Support;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Support.GetById;

public sealed class GetSupportByIdQueryHandler(ISupportRepository supportRepository)
    : GetByIdQueryHandler<GetSupportByIdQuery, SupportMessageEntity, SupportId, SupportResponse>(supportRepository)
{
    protected override SupportResponse MapToResponse(SupportMessageEntity entity)
    {
        return new SupportResponse(
            entity.Id.Value,
            entity.Name,
            entity.Email,
            entity.Type,
            entity.Message,
            entity.FromAuthenticated);
    }
}
