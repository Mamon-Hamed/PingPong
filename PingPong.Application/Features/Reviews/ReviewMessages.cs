using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Reviews;

public record CreateReviewCommand(
    double Rating,
    string Comment,
    Guid PartnerId) : ICommand<Guid>;

public record UpdateReviewCommand(
    Guid Id,
    double Rating,
    string Comment) : ICommand;

public record GetReviewByIdQuery(Guid Id) : GetByIdQuery<ReviewId, ReviewResponse>(Id);

public record GetAllReviewsQuery() : GetAllQuery<ReviewResponse>;

public record DeleteReviewCommand(Guid Id) : DeleteCommand<ReviewId>(Id);
