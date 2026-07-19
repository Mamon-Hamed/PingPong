using PingPong.Domain.Entities.Support;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Infrastructure.Persistence.Repositories;

public sealed class SupportRepository(AppDbContext context) 
    : Repository<SupportMessage, SupportId>(context), ISupportRepository
{
}
