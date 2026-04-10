#region

#endregion

namespace PingPong.Domain.Models;

public class PaginatedList<T>(List<T> items, int count, int pageNumber = 1, int pageSize = 10)
{
    public List<T> Items { get; } = items;
    public int PageNumber { get; } = pageNumber;
    public int TotalPages { get; } = (int)Math.Ceiling(count / (double)pageSize);
    public int TotalItems { get; } = count;

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

    public static PaginatedList<T> Empty => new(new List<T>(), 0);

    

    public static PaginatedList<T> Create(List<T> list, int pageNumber, int pageSize)
    {
        var count = list.Count();
        var items = list;

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }

    public static PaginatedList<T> Create(IEnumerable<T> list, int pageNumber, int pageSize)
    {
        var count = list.Count();
        var items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}