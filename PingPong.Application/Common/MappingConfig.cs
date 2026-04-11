using Mapster;
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
    }
}
