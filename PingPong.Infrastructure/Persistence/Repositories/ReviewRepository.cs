using PingPong.Domain.Entities.Partners;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Infrastructure.Persistence.Repositories;

internal sealed class ReviewRepository(AppDbContext dbContext)
    : Repository<PartnerReview, ReviewId>(dbContext), IReviewRepository
{
}
