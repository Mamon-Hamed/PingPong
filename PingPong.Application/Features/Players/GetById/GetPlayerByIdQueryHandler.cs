using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Domain.Entities;
using PingPong.Domain.Exceptions;
using PingPong.Domain.Repositories;

namespace PingPong.Application.Features.Players.GetById;

public sealed class GetPlayerByIdQueryHandler(IPlayerRepository playerRepository)
    : IQueryHandler<GetPlayerByIdQuery, PlayerResponse>
{
    public async Task<Result<PlayerResponse>> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
    {
        var player = await playerRepository.GetByIdAsync(new PlayerId(request.Id), cancellationToken);

        if (player is null)
            throw new NotFoundException(nameof(Player), request.Id);

        var response = new PlayerResponse(
            player.Id.Value,
            player.FirstName,
            player.LastName,
            player.Email,
            player.CreatedAtUtc);

        return Result.Success(response);
    }
}
