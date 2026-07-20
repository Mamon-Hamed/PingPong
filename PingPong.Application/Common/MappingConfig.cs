using Mapster;
using PingPong.Application.Features.Partners;
using PingPong.Domain.Entities.Partners;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Application.Common;

public static class MappingConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<StronglyTypedId, Guid>.NewConfig()
            .MapWith(src => src.Value);
        
        TypeAdapterConfig<StronglyTypedId, string>.NewConfig()
            .MapWith(src => src.Value.ToString());

        TypeAdapterConfig<PartnerEntity, PartnerDetailsResponse>.NewConfig()
            .Map(dest => dest.Rating, src => src.Reviews.Any() ? src.Reviews.Average(r => r.Rating) : 0)
            .Map(dest => dest.DiscountText, src => src.Services.Any(s => s.DiscountPercentage > 0) ? "up to " + src.Services.Max(s => s.DiscountPercentage) + "%" : "")
            .Map(dest => dest.Reviews, src => src.Reviews.OrderByDescending(r => r.Date))
            .Map(dest => dest.ReviewsCount, src => src.Reviews.Count)
            
            ;
        TypeAdapterConfig<PartnerEntity, PartnerResponse>.NewConfig()
            .Map(dest => dest.Rating, src => src.Reviews.Any() ? src.Reviews.Average(r => r.Rating) : 0)
            .Map(dest => dest.ReviewsCount, src => src.Reviews.Count)
            .Map(dest => dest.DiscountText, src => src.Services.Any(s => s.DiscountPercentage > 0) ? "up to " + src.Services.Max(s => s.DiscountPercentage) + "%" : "");

        TypeAdapterConfig<PartnerServiceEntity, ServiceResponse>.NewConfig()
            .Map(dest => dest.CurrencyCode, src => src.Currency != null ? src.Currency.Code : "")
            .Map(dest => dest.CurrencySymbol, src => src.Currency != null ? src.Currency.Symbol : "")
            .Map(dest => dest.CurrencyRate, src => src.Currency != null ? src.Currency.Rate : 0);
    }
}
