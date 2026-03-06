using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Domain.Entities;
using PingPong.Domain.Exceptions;
using PingPong.Domain.Repositories;

namespace PingPong.Application.Features.Players.Create;

public sealed class CreatePlayerCommandHandler(IPlayerRepository playerRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePlayerCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var existingPlayer = await playerRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (existingPlayer is not null)
            throw new ConflictException(nameof(Player), request.Email);

        var player = Player.Create(request.FirstName, request.LastName, request.Email);

        playerRepository.Add(player);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(player.Id.Value);
    }
}
