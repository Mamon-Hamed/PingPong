using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PingPong.Domain.Entities.Notifications;
using PingPong.Domain.StronglyTypes;
using PingPong.Domain.Constants;
using PingPong.Infrastructure.Persistence.Configurations.Common;
using PingPong.Infrastructure.Persistence.Configurations.Extensions;

namespace PingPong.Infrastructure.Persistence.Configurations.Notifications;

internal sealed class NotificationConfiguration : BaseEntityConfiguration<NotificationEntity, NotificationId>
{
    protected override void ConfigureEntity(EntityTypeBuilder<NotificationEntity> builder)
    {
        builder.ToTable("Notifications");

        builder.Property(n => n.Title)
            .HasMaxLength(StringLengths.Length200)
            .IsRequired();

        builder.Property(n => n.Message)
            .HasMaxLength(StringLengths.Length2000)
            .IsRequired();

        builder.Property(n => n.ImageUrl)
            .HasMaxLength(StringLengths.Length2000);

        builder.Property(n => n.Type)
            .IsTinyEnum()
            .IsRequired();

        builder.HasIndex(n => n.Type);
        builder.HasIndex(n => n.CreatedAt);
    }
}
