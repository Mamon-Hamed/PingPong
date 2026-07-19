namespace PingPong.Application.Features.Reviews;

public record ReviewResponse(
    Guid Id,
    string AuthorName,
    string AuthorAvatar,
    double Rating,
    string Comment,
    DateTime Date,
    Guid PartnerId,
    string UserId);
