using PingPong.Application.Abstractions.Authentication;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Domain.Entities.Partners;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Features.Reviews;

public sealed class CreateReviewCommandHandler(
    IReviewRepository reviewRepository,
    ICurrentUserService currentUserService,
    IIdentityService identityService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateReviewCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;
        if (string.IsNullOrEmpty(userId))
        {
            return Result.Failure<Guid>("User is not authenticated.");
        }

        var userResult = await identityService.GetUserByIdAsync(userId);
        if (userResult.IsFailure)
        {
            return Result.Failure<Guid>(userResult.Error!);
        }

        var user = userResult.Value;

        var review = PartnerReviewEntity.Create(
            user?.UserName ?? string.Empty,
            user?.AvatarUrl ?? string.Empty,
            request.Rating,
            request.Comment,
            DateTime.UtcNow,
            new PartnerId(request.PartnerId),
            userId);

        reviewRepository.Add(review);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(review.Id.Value);
    }
}

public sealed class UpdateReviewCommandHandler(
    IReviewRepository reviewRepository,
    ICurrentUserService currentUserService,
    IIdentityService identityService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateReviewCommand>
{
    public async Task<Result> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var reviewId = new ReviewId(request.Id);
        var review = await reviewRepository.GetByIdAsync(reviewId, cancellationToken);

        if (review is null)
        {
            return Result.Failure("The requested entity was not found.");
        }

        var userId = currentUserService.UserId;
        if (string.IsNullOrEmpty(userId) || review.UserId != userId)
        {
            return Result.Failure("You are not authorized to update this review.");
        }

        var userResult = await identityService.GetUserByIdAsync(userId);
        if (userResult.IsFailure)
        {
            return Result.Failure(userResult.Error!);
        }

        var user = userResult.Value;

        review.Update(user?.UserName ?? string.Empty, user?.AvatarUrl ?? string.Empty, request.Rating, request.Comment);

        reviewRepository.Update(review);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

public sealed class GetReviewByIdQueryHandler(IReviewRepository repository)
    : GetByIdQueryHandler<GetReviewByIdQuery, PartnerReviewEntity, ReviewId, ReviewResponse>(repository)
{
    protected override ReviewResponse MapToResponse(PartnerReviewEntity entity)
    {
        return new ReviewResponse(
            entity.Id.Value,
            entity.AuthorName,
            entity.AuthorAvatar,
            entity.Rating,
            entity.Comment,
            entity.Date,
            entity.PartnerId.Value,
            entity.UserId);
    }
}

public sealed class GetAllReviewsQueryHandler(IReviewRepository repository)
    : GetAllQueryHandler<GetAllReviewsQuery, PartnerReviewEntity, ReviewId, ReviewResponse>(repository)
{
    protected override IQueryable<PartnerReviewEntity> BuildQuery(GetAllReviewsQuery query)
    {
        return Queryable;
    }
}

public sealed class DeleteReviewCommandHandler(IReviewRepository repository, IUnitOfWork unitOfWork)
    : DeleteCommandHandler<DeleteReviewCommand, PartnerReviewEntity, ReviewId>(repository, unitOfWork);