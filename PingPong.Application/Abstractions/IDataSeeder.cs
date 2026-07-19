using PingPong.Application.Common;

namespace PingPong.Application.Abstractions;

public interface IDataSeeder
{
    Task<Result> SeedDataAsync(int categoriesCount, int partnersCount, int reviewsCount);
    Task<Result> SeedLocationsAsync();
    Task<Result> SeedUsersAsync();
}
