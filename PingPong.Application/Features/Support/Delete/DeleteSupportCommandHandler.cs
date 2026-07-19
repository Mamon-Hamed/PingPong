using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Entities.Support;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Support.Delete;

public sealed class DeleteSupportCommandHandler(
    ISupportRepository supportRepository,
    IUnitOfWork unitOfWork)
    : DeleteCommandHandler<DeleteSupportCommand, SupportMessage, SupportId>(supportRepository, unitOfWork)
{
}
