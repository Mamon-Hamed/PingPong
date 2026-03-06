using PingPong.Application.Common;

namespace PingPong.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : Cortex.Mediator.Queries.IQueryHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
