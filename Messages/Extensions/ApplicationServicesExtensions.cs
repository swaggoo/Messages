using Messages.Data;
using Messages.IRepository;
using Messages.Repository;

namespace Messages.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddSingleton<IContext, Context>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddDistributedMemoryCache();
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(20);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        return services;
    }
}
