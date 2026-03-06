using PingPong.API.Middleware;
using PingPong.Application;
using PingPong.Infrastructure;

namespace PingPong.API.Install;

public static class Compiler
{
    public static IServiceCollection InstallServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication(configuration);
        services.AddInfrastructure(configuration);
        services.AddApi(configuration);
        return services;
    }

    public static IApplicationBuilder BuildServices(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.DisplayRequestDuration();
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "PingPong API v1");
            options.RoutePrefix = string.Empty;
        });


        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        return app;
    }
}