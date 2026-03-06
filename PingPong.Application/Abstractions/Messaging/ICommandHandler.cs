using PingPong.Application.Common;

namespace PingPong.Application.Abstractions.Messaging;

public interface ICommandHandler<in TCommand> : Cortex.Mediator.Commands.ICommandHandler<TCommand, Result>
    where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResponse> : Cortex.Mediator.Commands.ICommandHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;
