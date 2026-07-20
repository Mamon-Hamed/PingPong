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
using PingPong.Domain.Entities;
using PingPong.Domain.StronglyTypes;

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

            // 0. Seed Currencies
            var currencies = await dbContext.Currencies.ToListAsync();
            if (currencies.Count == 0)
            {
                currencies.Add(CurrencyEntity.Create("US Dollar", "USD", "$", 1.0m, true));
                currencies.Add(CurrencyEntity.Create("Euro", "EUR", "€", 0.92m));
                currencies.Add(CurrencyEntity.Create("Syrian Pound", "SYP", "LS", 13000m));
                await dbContext.Currencies.AddRangeAsync(currencies);
                await dbContext.SaveChangesAsync();
            }

            // 1. Seed Categories
            var categories = await dbContext.Categories.ToListAsync();
            if (categories.Count < categoriesCount)
            {
                var categoryData = new[]
                {
                    (Name: "Electronics", Icon: "https://cdn-icons-png.flaticon.com/512/1261/1261143.png", Color: "#4B6584"),
                    (Name: "Fashion", Icon: "https://cdn-icons-png.flaticon.com/512/3050/3050239.png", Color: "#EB3B5A"),
                    (Name: "Food & Drinks", Icon: "https://cdn-icons-png.flaticon.com/512/3075/3075977.png", Color: "#F7B731"),
                    (Name: "Health", Icon: "https://cdn-icons-png.flaticon.com/512/2966/2966327.png", Color: "#20BF6B"),
                    (Name: "Home & Garden", Icon: "https://cdn-icons-png.flaticon.com/512/2611/2611153.png", Color: "#A55EEA"),
                    (Name: "Sports", Icon: "https://cdn-icons-png.flaticon.com/512/857/857418.png", Color: "#2D98DA"),
                    (Name: "Beauty", Icon: "https://cdn-icons-png.flaticon.com/512/1940/1940922.png", Color: "#FD9644"),
                    (Name: "Automotive", Icon: "https://cdn-icons-png.flaticon.com/512/2084/2084110.png", Color: "#778CA3"),
                    (Name: "Entertainment", Icon: "https://cdn-icons-png.flaticon.com/512/3163/3163478.png", Color: "#FA8231"),
                    (Name: "Services", Icon: "https://cdn-icons-png.flaticon.com/512/1066/1066371.png", Color: "#45AAF2")
                };
                
                var categoriesToAdd = new List<CategoryEntity>();
                for (int i = categories.Count; i < categoriesCount; i++)
                {
                    if (i < categoryData.Length)
                    {
                        var data = categoryData[i];
                        categoriesToAdd.Add(CategoryEntity.Create(data.Name, data.Icon, data.Color));
                    }
                    else
                    {
                        categoriesToAdd.Add(CategoryEntity.Create(faker.Commerce.Categories(1)[0], "https://cdn-icons-png.flaticon.com/512/1160/1160358.png", faker.Internet.Color()));
                    }
                }
                await dbContext.Categories.AddRangeAsync(categoriesToAdd);
                await dbContext.SaveChangesAsync();
                categories.AddRange(categoriesToAdd);
            }

            // 3. Seed Partners
            var partners = new List<PartnerEntity>();
            
            var realisticPartners = new[]
            {
                (Name: "Cham Palace", Category: "Services", Image: "https://images.unsplash.com/photo-1566073771259-6a8506099945?q=80&w=1000&auto=format&fit=crop"),
                (Name: "Syrian Modern Electronics", Category: "Electronics", Image: "https://images.unsplash.com/photo-1498049794561-7780e7231661?q=80&w=1000&auto=format&fit=crop"),
                (Name: "Al-Hameed Fashion", Category: "Fashion", Image: "https://images.unsplash.com/photo-1441986300917-64674bd600d8?q=80&w=1000&auto=format&fit=crop"),
                (Name: "Damascus Delights", Category: "Food & Drinks", Image: "https://images.unsplash.com/photo-1504674900247-0877df9cc836?q=80&w=1000&auto=format&fit=crop"),
                (Name: "Al-Shifa Pharmacy", Category: "Health", Image: "https://images.unsplash.com/photo-1586015555751-63bb77f4322a?q=80&w=1000&auto=format&fit=crop"),
                (Name: "Green Oasis Garden Center", Category: "Home & Garden", Image: "https://images.unsplash.com/photo-1416870213410-66fc33633d71?q=80&w=1000&auto=format&fit=crop"),
                (Name: "Barada Sports Club", Category: "Sports", Image: "https://images.unsplash.com/photo-1534438327276-14e5300c3a48?q=80&w=1000&auto=format&fit=crop"),
                (Name: "Zenobia Beauty Salon", Category: "Beauty", Image: "https://images.unsplash.com/photo-1560066984-138dadb4c035?q=80&w=1000&auto=format&fit=crop"),
                (Name: "Syrian Auto Care", Category: "Automotive", Image: "https://images.unsplash.com/photo-1486006396193-471068589bca?q=80&w=1000&auto=format&fit=crop"),
                (Name: "Opera Theater Cafe", Category: "Entertainment", Image: "https://images.unsplash.com/photo-1514525253361-bee8718a300c?q=80&w=1000&auto=format&fit=crop")
            };

            for (int i = 0; i < partnersCount; i++)
            {
                string partnerName;
                string partnerCategory;
                string partnerImage;

                if (i < realisticPartners.Length)
                {
                    partnerName = realisticPartners[i].Name;
                    partnerCategory = realisticPartners[i].Category;
                    partnerImage = realisticPartners[i].Image;
                }
                else
                {
                    partnerName = faker.Company.CompanyName();
                    partnerCategory = faker.PickRandom(categories).Name;
                    partnerImage = faker.Image.PicsumUrl(400, 300);
                }

                var category = categories.FirstOrDefault(c => c.Name == partnerCategory) ?? faker.PickRandom(categories);
                var city = faker.PickRandom(syriaCities);
                
                var partner = PartnerEntity.Create(
                    partnerName,
                    faker.Phone.PhoneNumber("+963#########"),
                    partnerImage,
                    DateTime.UtcNow.AddMonths(faker.Random.Int(1, 12)),
                    [partnerImage, faker.Image.PicsumUrl(400, 300)],
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
                var currency = faker.PickRandom(currencies);
                for (int j = 0; j < servicesCount; j++)
                {
                    partner.AddService(
                        faker.Commerce.ProductName(),
                        faker.Image.PicsumUrl(100, 100),
                        faker.Random.Decimal(10, 500),
                        faker.Random.Int(5, 30),
                        currency.Id
                    );
                }
                
                // Add opening hours
                var days = Enum.GetValues<DayOfWeek>();
                foreach (var day in days)
                {
                    bool isClosed = day == DayOfWeek.Friday;
                    partner.AddOpeningHour(
                        day,
                        isClosed ? "" : "09:00",
                        isClosed ? "" : "21:00",
                        isClosed
                    );
                }
                
                partners.Add(partner);
            }
            await dbContext.Partners.AddRangeAsync(partners);

            // 4. Seed Reviews using the regular user
            var reviews = new List<PartnerReviewEntity>();
            for (int i = 0; i < reviewsCount; i++)
            {
                var partner = faker.PickRandom(partners);
                var review = PartnerReviewEntity.Create(
                    regularUser.UserName ?? "user",
                    regularUser.AvatarUrl ?? "https://i.pravatar.cc/150?u=user",
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
                    AvatarUrl = "https://i.pravatar.cc/150?u=admin"
                };
                
                adminUser.Locations.Add(new UserLocation
                {
                    Address = "Damascus, Syria",
                    Latitude = 33.5138,
                    Longitude = 36.2765
                });

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
                    AvatarUrl = "https://i.pravatar.cc/150?u=user"
                };

                regularUser.Locations.Add(new UserLocation
                {
                    Address = "Aleppo, Syria",
                    Latitude = 36.2021,
                    Longitude = 37.1343
                });

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
