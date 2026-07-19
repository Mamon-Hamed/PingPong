using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PingPong.Application.Abstractions;
using PingPong.Application.Common;
using PingPong.Domain.Constants;
using PingPong.Domain.Entities.Categories;
using PingPong.Domain.Entities.Locations;
using PingPong.Domain.Entities.Partners;
using PingPong.Domain.Models;
using PingPong.Infrastructure.Identity;

namespace PingPong.Infrastructure.Persistence;

public class DataSeeder(
    AppDbContext dbContext,
    UserManager<ApplicationUser> userManager) : IDataSeeder
{
    public async Task<Result> SeedDataAsync(int categoriesCount, int partnersCount, int reviewsCount)
    {
        try
        {
            var faker = new Faker();

            // Ensure Locations and Users are seeded first as they are required for Partners and Reviews
            await SeedLocationsAsync();
            await SeedUsersAsync();

            var country = await dbContext.Countries.FirstAsync(c => c.Name == "Syria");
            var syriaCities = await dbContext.Cities.Where(c => c.CountryId == country.Id).ToListAsync();
            var regularUser = await userManager.FindByEmailAsync("user@pinpon.com");

            if (regularUser == null)
            {
                return Result.Failure("Regular user not found after seeding users.");
            }

            // 1. Seed Categories
            var categories = await dbContext.Categories.ToListAsync();
            if (categories.Count < categoriesCount)
            {
                var categoryNames = new[] { "Electronics", "Fashion", "Food & Drinks", "Health", "Home & Garden", "Sports", "Beauty", "Automotive", "Entertainment", "Services" };
                var categoriesToAdd = new List<CategoryEntity>();
                for (int i = categories.Count; i < categoriesCount; i++)
                {
                    var name = i < categoryNames.Length ? categoryNames[i] : faker.Commerce.Categories(1)[0];
                    var category = CategoryEntity.Create(name, $"https://api.dicebear.com/7.x/identicon/svg?seed={name}");
                    categoriesToAdd.Add(category);
                }
                await dbContext.Categories.AddRangeAsync(categoriesToAdd);
                await dbContext.SaveChangesAsync();
                categories.AddRange(categoriesToAdd);
            }

            // 3. Seed Partners
            var partners = new List<PartnerEntity>();
            for (int i = 1; i <= partnersCount; i++)
            {
                var category = faker.PickRandom(categories);
                var city = faker.PickRandom(syriaCities);
                var partnerName = faker.Company.CompanyName();
                var partner = PartnerEntity.Create(
                    partnerName,
                    faker.Phone.PhoneNumber("+963#########"),
                    faker.Image.PicsumUrl(400, 300),
                    DateTime.UtcNow.AddMonths(faker.Random.Int(1, 12)),
                    [faker.Image.PicsumUrl(400, 300), faker.Image.PicsumUrl(400, 300)],
                    category.Id,
                    country.Id,
                    city.Id,
                    new Location(
                        faker.Address.Latitude(32.5, 37.5),
                        faker.Address.Longitude(35.5, 42.5),
                        city.Name,
                        country.Name,
                        faker.Address.StreetAddress()),
                    SubscriptionStatus.Active
                );
                
                // Add some realistic services
                int servicesCount = faker.Random.Int(1, 4);
                for (int j = 0; j < servicesCount; j++)
                {
                    partner.AddService(
                        faker.Commerce.ProductName(),
                        faker.Image.PicsumUrl(100, 100),
                        faker.Random.Decimal(10, 500),
                        faker.Random.Int(5, 30)
                    );
                }
                
                partners.Add(partner);
            }
            await dbContext.Partners.AddRangeAsync(partners);

            // 4. Seed Reviews using the regular user
            var reviews = new List<PartnerReview>();
            for (int i = 0; i < reviewsCount; i++)
            {
                var partner = faker.PickRandom(partners);
                var review = PartnerReview.Create(
                    regularUser.UserName ?? "user",
                    regularUser.AvatarUrl ?? "https://api.dicebear.com/7.x/avataaars/svg?seed=user",
                    faker.Random.Int(1, 5),
                    faker.Rant.Review("partner"),
                    DateTime.UtcNow.AddDays(-faker.Random.Int(1, 30)),
                    partner.Id,
                    regularUser.Id
                );
                reviews.Add(review);
            }
            await dbContext.PartnerReviews.AddRangeAsync(reviews);

            await dbContext.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Seeding failed: {ex.Message}");
        }
    }

    public async Task<Result> SeedLocationsAsync()
    {
        try
        {
            var country = await dbContext.Countries.FirstOrDefaultAsync(c => c.Name == "Syria");
            if (country == null)
            {
                country = CountryEntity.Create("Syria", "SY");
                await dbContext.Countries.AddAsync(country);
                await dbContext.SaveChangesAsync();
            }

            var cities = new[] { "Damascus", "Aleppo", "Homs", "Hama", "Latakia", "Tartus", "Idlib", "Deir ez-Zor", "Raqqa", "Hasakah", "Daraa", "Sweida", "Quneitra" };
            foreach (var cityName in cities)
            {
                var cityExists = await dbContext.Cities.AnyAsync(c => c.Name == cityName && c.CountryId == country.Id);
                if (!cityExists)
                {
                    var city = CityEntity.Create(cityName, country.Id);
                    await dbContext.Cities.AddAsync(city);
                }
            }
            await dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Seeding locations failed: {ex.Message}");
        }
    }

    public async Task<Result> SeedUsersAsync()
    {
        try
        {
            var adminEmail = "admin@pinpon.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    PhoneNumber = "+963912345678",
                    IsActive = true,
                    AvatarUrl = "https://api.dicebear.com/7.x/avataaars/svg?seed=admin"
                };
                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, Roles.Admin);
                }
            }

            var userEmail = "user@pinpon.com";
            var regularUser = await userManager.FindByEmailAsync(userEmail);
            if (regularUser == null)
            {
                regularUser = new ApplicationUser
                {
                    UserName = "user",
                    Email = userEmail,
                    PhoneNumber = "+963987654321",
                    IsActive = true,
                    AvatarUrl = "https://api.dicebear.com/7.x/avataaars/svg?seed=user"
                };
                var result = await userManager.CreateAsync(regularUser, "User123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(regularUser, Roles.User);
                }
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Seeding users failed: {ex.Message}");
        }
    }
}
