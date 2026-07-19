using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Application.Shared;

namespace PingPong.Application.Features.Categories;

public sealed record GetCategoryLookupQuery : IQuery<List<LookupResponse>>;
