using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Repositories;
using PingPong.Domain.Entities;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Partners.Delete;

internal sealed class DeletePartnerCommandHandler(
    IPartnerRepository partnerRepository,
    IUnitOfWork unitOfWork)
    : DeleteCommandHandler<DeletePartnerCommand, PartnerEntity, PartnerId>(partnerRepository, unitOfWork);
