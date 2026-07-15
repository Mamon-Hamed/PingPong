using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Cities;

public sealed record DeleteCityCommand(Guid Id) : DeleteCommand<CityId>(Id);
