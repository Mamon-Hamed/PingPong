using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PingPong.Domain.Entities.Support;
using PingPong.Infrastructure.Persistence.Configurations.Common;
using PingPong.Infrastructure.Persistence.Configurations.Extensions;
using PingPong.Domain.Constants;

namespace PingPong.Infrastructure.Persistence.Configurations.Support;

public sealed class SupportMessageConfiguration : BaseEntityConfiguration<SupportMessageEntity, Domain.StronglyTypes.SupportId>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SupportMessageEntity> builder)
    {
        builder.Property(s => s.Name)
            .HasMaxLength(StringLengths.Length200)
            .IsRequired();

        builder.Property(s => s.Email)
            .HasMaxLength(StringLengths.Length256)
            .IsRequired();

        builder.Property(s => s.Type)
            .IsTinyEnum()
            .IsRequired();

        builder.Property(s => s.Message)
            .HasMaxLength(StringLengths.Length2000)
            .IsRequired();

        builder.Property(s => s.FromAuthenticated)
            .IsRequired();

        builder.HasIndex(x => x.Email);
        builder.HasIndex(x => x.Type);
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.CreatedAt);
    }
}
