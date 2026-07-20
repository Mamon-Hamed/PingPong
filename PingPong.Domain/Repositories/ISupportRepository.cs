using PingPong.Domain.Entities.Support;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Repositories;

public interface ISupportRepository : IRepository<SupportMessageEntity, SupportId>
{
}
