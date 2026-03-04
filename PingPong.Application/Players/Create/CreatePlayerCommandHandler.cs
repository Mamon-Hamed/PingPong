using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Entities;
using PingPong.Domain.Exceptions;
using PingPong.Domain.Repositories;

namespace PingPong.Application.Players.Create;

internal sealed class CreatePlayerCommandHandler : ICommandHandler<CreatePlayerCommand, Guid>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePlayerCommandHandler(IPlayerRepository playerRepository, IUnitOfWork unitOfWork)
    {
        _playerRepository = playerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var existingPlayer = await _playerRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (existingPlayer is not null)
            throw new ConflictException(nameof(Player), request.Email);

        var player = Player.Create(request.FirstName, request.LastName, request.Email);

        _playerRepository.Add(player);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(player.Id.Value);
    }
}
