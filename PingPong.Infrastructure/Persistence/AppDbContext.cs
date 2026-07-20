using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PingPong.Domain.Entities.Categories;
using PingPong.Domain.Entities.Locations;
using PingPong.Domain.Entities.Partners;
using PingPong.Domain.Entities.Subscriptions;
using PingPong.Domain.Entities.Support;
using PingPong.Domain.Entities.Notifications;
using PingPong.Infrastructure.Identity;

using PingPong.Domain.Entities;

namespace PingPong.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<CurrencyEntity> Currencies => Set<CurrencyEntity>();
    public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
    public DbSet<SupportMessageEntity> SupportMessages => Set<SupportMessageEntity>();
    public DbSet<SubscriptionPlanEntity> SubscriptionPlans => Set<SubscriptionPlanEntity>();
    public DbSet<PartnerEntity> Partners => Set<PartnerEntity>();
    public DbSet<PartnerOpeningHourEntity> PartnerOpeningHours => Set<PartnerOpeningHourEntity>();
    public DbSet<PartnerServiceEntity> PartnerServices => Set<PartnerServiceEntity>();
    public DbSet<PartnerReviewEntity> PartnerReviews => Set<PartnerReviewEntity>();
    public DbSet<CountryEntity> Countries => Set<CountryEntity>();
    public DbSet<CityEntity> Cities => Set<CityEntity>();
    public DbSet<NotificationEntity> Notifications => Set<NotificationEntity>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
