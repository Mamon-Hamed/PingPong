using Cortex.Mediator.Notifications;
using PingPong.Domain.Primitives;

namespace PingPong.Application.Abstractions.Messaging;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent;
