using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PingPong.Domain.Entities;
using PingPong.Infrastructure.Identity;

namespace PingPong.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
    public DbSet<SubscriptionPlanEntity> SubscriptionPlans => Set<SubscriptionPlanEntity>();
    public DbSet<PartnerEntity> Partners => Set<PartnerEntity>();
    public DbSet<CountryEntity> Countries => Set<CountryEntity>();
    public DbSet<CityEntity> Cities => Set<CityEntity>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
