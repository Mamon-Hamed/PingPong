using PingPong.Domain.Models;
using PingPong.Domain.Repositories;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Abstractions.Messaging;

public record GetByIdQuery<TId, TResponse>(Guid Id) : IQuery<TResponse>
    where TId : StronglyTypedId;



public record GetAllQuery<TResponse>() : IQuery<PaginatedList<TResponse>>, IAuditQuery
{
    public DateTime? MinCreatedAt { get; set; }

    public DateTime? MaxCreatedAt { get; set; }


    public DateTime? MinUpdatedAt { get; set; }

    public DateTime? MaxUpdatedAt { get; set; }
    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }
    public string? CreatedByName { get; set; } = string.Empty;
    public string? UpdatedByName { get; set; } = string.Empty;

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;


    public string? SortBy { get; set; }
    public bool IsAscending { get; set; } = true;   
}

public record DeleteCommand<TId>(Guid Id) : ICommand
    where TId : StronglyTypedId;
