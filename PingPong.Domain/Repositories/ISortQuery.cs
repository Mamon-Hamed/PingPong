namespace PingPong.Domain.Repositories;

public interface ISortQuery
{
    string? SortBy { get; set; }
    bool IsAscending { get; set; }
}