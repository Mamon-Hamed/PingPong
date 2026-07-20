using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Shared.Extensions;
using PingPong.Domain.Entities.Support;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Support.GetAll;

public sealed class GetAllSupportMessagesQueryHandler(ISupportRepository repository)
    : GetAllQueryHandler<GetAllSupportMessagesQuery, SupportMessageEntity, SupportId, SupportResponse>(repository)
{
    protected override IQueryable<SupportMessageEntity> BuildQuery(GetAllSupportMessagesQuery query)
    {
        return Queryable.FilterBase(query)
            .WhereIf(!string.IsNullOrEmpty(query.Name), s => s.Name.Contains(query.Name!))
            .WhereIf(!string.IsNullOrEmpty(query.Email), s => s.Email.Contains(query.Email!))
            .WhereIf(query.Type.HasValue, s => s.Type == query.Type)
            .WhereIf(query.FromAuthenticated.HasValue, s => s.FromAuthenticated == query.FromAuthenticated);
    }
}
