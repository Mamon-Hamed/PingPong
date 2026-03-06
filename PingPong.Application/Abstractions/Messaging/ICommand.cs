using PingPong.Application.Common;

namespace PingPong.Application.Abstractions.Messaging;

public interface ICommand :  Cortex.Mediator.Commands.ICommand<Result>;

public interface ICommand<TResponse> : Cortex.Mediator.Commands.ICommand<Result<TResponse>>;
