namespace PingPong.Domain.Repositories;

public interface IPagedQuery
{
    int Page { get; set; }
    int PageSize { get; set; }
}