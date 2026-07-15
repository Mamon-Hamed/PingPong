using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Countries;

public sealed record DeleteCountryCommand(Guid Id) : DeleteCommand<CountryId>(Id);
