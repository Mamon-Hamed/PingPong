using PingPong.Domain.StronglyTypes;
using PingPong.Domain.Entities.Partners;

namespace PingPong.Domain.Repositories;

public interface IPartnerRepository : IRepository<PartnerEntity, PartnerId>
{
}
