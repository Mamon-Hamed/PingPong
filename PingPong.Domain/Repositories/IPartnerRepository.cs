using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Repositories;

using Entities;

public interface IPartnerRepository : IRepository<PartnerEntity, PartnerId>
{
}
