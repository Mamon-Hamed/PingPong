using Microsoft.AspNetCore.Mvc;
using PingPong.Application.Abstractions;

namespace PingPong.API.Controllers;

public class SeedController(IDataSeeder dataSeeder) : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> Seed([FromQuery] int categoriesCount = 3, [FromQuery] int partnersCount = 10, [FromQuery] int reviewsCount = 5)
    {
        var result = await dataSeeder.SeedDataAsync(categoriesCount, partnersCount, reviewsCount);
        return HandleResult(result);
    }

    [HttpPost("locations")]
    public async Task<IActionResult> SeedLocations()
    {
        var result = await dataSeeder.SeedLocationsAsync();
        return HandleResult(result);
    }

    [HttpPost("users")]
    public async Task<IActionResult> SeedUsers()
    {
        var result = await dataSeeder.SeedUsersAsync();
        return HandleResult(result);
    }
}
