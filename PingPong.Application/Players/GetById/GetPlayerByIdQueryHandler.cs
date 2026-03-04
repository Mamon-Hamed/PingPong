using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Entities;
using PingPong.Domain.Exceptions;
using PingPong.Domain.Repositories;

namespace PingPong.Application.Players.GetById;

internal sealed class GetPlayerByIdQueryHandler : IQueryHandler<GetPlayerByIdQuery, PlayerResponse>
{
    private readonly IPlayerRepository _playerRepository;

    public GetPlayerByIdQueryHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<Result<PlayerResponse>> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
    {
        var player = await _playerRepository.GetByIdAsync(new PlayerId(request.Id), cancellationToken);

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
