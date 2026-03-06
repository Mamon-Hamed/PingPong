using Cortex.Mediator.DependencyInjection;
using Cortex.Mediator.Processors;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PingPong.Application.Behaviors;

namespace PingPong.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration _)
    {
        // services.AddMediatorCaching(options =>
        // {
        //     options.DefaultAbsoluteExpiration = TimeSpan.FromMinutes(5);
        //     options.DefaultSlidingExpiration = TimeSpan.FromMinutes(1);
        //     options.CacheKeyPrefix = "PingPong";
        // });
        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection),includeInternalTypes:true);
        services.AddTransient(typeof(IRequestPreProcessor<>), typeof(ValidationBehavior<>));
        services.AddCortexMediator(
            [typeof(DependencyInjection)], // Assemblies to scan for handlers
            options =>
            {
                options.AddDefaultBehaviors();
                options.AddCachingBehavior();
                options.AddProcessorBehaviors();
            }
            
        );
       


        return services;
    }
}
