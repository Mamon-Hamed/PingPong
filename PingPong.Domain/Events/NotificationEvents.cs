using PingPong.Domain.Primitives;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Events;

public sealed record PartnerCreatedDomainEvent(PartnerId PartnerId, string Name, string? MediaUrl) : IDomainEvent;

public sealed record PartnerServiceUpdatedDomainEvent(PartnerId PartnerId, string PartnerName, ServiceId ServiceId, string ServiceName) : IDomainEvent;

public sealed record GeneralNotificationAddedDomainEvent(string Title, string Message, string? ImageUrl) : IDomainEvent;

public sealed record PartnerViewedDomainEvent(PartnerId PartnerId) : IDomainEvent;
