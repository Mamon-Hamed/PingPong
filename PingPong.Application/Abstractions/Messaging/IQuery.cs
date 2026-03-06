using PingPong.Application.Common;

namespace PingPong.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : Cortex.Mediator.Queries.IQuery<Result<TResponse>>;
