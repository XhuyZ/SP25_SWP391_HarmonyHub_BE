using Domain.Entities;
using Repository.Implementations;
using Repository.Interfaces;
using Service.Implementations;
using Service.Interfaces;
using Service.Settings;

namespace API.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.Configure<AdminAccount>(config.GetSection("AdminAccount"));
        services.Configure<JwtSettings>(config.GetSection("JwtSettings"));
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAccountRepository, AccountRepository>();

        services.AddScoped<IBlogService, BlogService>();
        services.AddScoped<IBlogRepository, BlogRepository>();

        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();

        services.AddScoped<IPackageService, PackageService>();
        services.AddScoped<IPackageRepository, PackageRepository>();

        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IReportRepository, ReportRepository>();

        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<IQuizService, QuizService>();


        return services;
    }
}