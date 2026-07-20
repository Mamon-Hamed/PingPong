using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PingPong.Domain.Entities.Partners;
using PingPong.Infrastructure.Persistence.Configurations.Common;
using PingPong.Domain.StronglyTypes;
using PingPong.Domain.Constants;
using PingPong.Infrastructure.Persistence.Converters;

namespace PingPong.Infrastructure.Persistence.Configurations.Partners;

internal sealed class PartnerReviewConfiguration : BaseEntityConfiguration<PartnerReviewEntity, ReviewId>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PartnerReviewEntity> builder)
    {
        builder.Property(r => r.AuthorName)
            .HasMaxLength(StringLengths.Length200)
            .IsRequired();

        builder.Property(r => r.AuthorAvatar)
            .HasMaxLength(StringLengths.Length2000);

        builder.Property(r => r.Rating)
            .IsRequired();

        builder.Property(r => r.Comment)
            .HasMaxLength(StringLengths.Length2000);

        builder.Property(r => r.Date)
            .IsRequired();

        builder.Property(r => r.PartnerId)
            .HasConversion(new StronglyTypedIdValueConverter<PartnerId>())
            .IsRequired();
            
        builder.Property(r => r.UserId)
            .IsRequired();

        builder.HasIndex(r => r.PartnerId);
        builder.HasIndex(r => r.UserId);
        builder.HasIndex(r => r.Date);
    }
}
