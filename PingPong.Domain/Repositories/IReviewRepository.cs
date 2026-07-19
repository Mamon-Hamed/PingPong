using PingPong.Domain.Entities.Partners;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Repositories;

public interface IReviewRepository : IRepository<PartnerReview, ReviewId>
{
}
