using Microsoft.AspNetCore.Authorization;
using PingPong.Application.Features.Reviews;
using PingPong.Domain.Constants;
using PingPong.Domain.StronglyTypes;

namespace PingPong.API.Controllers;

[Authorize(Roles = Roles.Admin)]
public class ReviewsController : CrudController<ReviewId, ReviewResponse, CreateReviewCommand, UpdateReviewCommand, DeleteReviewCommand, GetReviewByIdQuery, GetAllReviewsQuery>
{
}
