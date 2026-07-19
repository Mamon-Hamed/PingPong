using PingPong.Application.Abstractions.Messaging;
using PingPong.Domain.Entities.Support;

namespace PingPong.Application.Features.Support.GetAll;

public sealed record GetAllSupportMessagesQuery : GetAllQuery<SupportResponse>
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public SupportType? Type { get; set; }
    public bool? FromAuthenticated { get; set; }
}
