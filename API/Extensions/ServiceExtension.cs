using Repository.Implementations;
using Repository.Interfaces;
using Service.Implementations;
using Service.Interfaces;

namespace API.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IFeedbackService, FeedbackService>();
        services.AddScoped<IFeedbackRepository, FeedbackRepository>();

        return services;
    }
}