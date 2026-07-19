using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PingPong.Domain.Entities.Categories;
using PingPong.Domain.Entities.Locations;
using PingPong.Domain.Entities.Partners;
using PingPong.Domain.Entities.Subscriptions;
using PingPong.Domain.Entities.Support;
using PingPong.Domain.Entities.Notifications;
using PingPong.Infrastructure.Identity;

namespace PingPong.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
    public DbSet<SupportMessage> SupportMessages => Set<SupportMessage>();
    public DbSet<SubscriptionPlanEntity> SubscriptionPlans => Set<SubscriptionPlanEntity>();
    public DbSet<PartnerEntity> Partners => Set<PartnerEntity>();
    public DbSet<PartnerOpeningHour> PartnerOpeningHours => Set<PartnerOpeningHour>();
    public DbSet<PartnerService> PartnerServices => Set<PartnerService>();
    public DbSet<PartnerReview> PartnerReviews => Set<PartnerReview>();
    public DbSet<CountryEntity> Countries => Set<CountryEntity>();
    public DbSet<CityEntity> Cities => Set<CityEntity>();
    public DbSet<NotificationEntity> Notifications => Set<NotificationEntity>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
