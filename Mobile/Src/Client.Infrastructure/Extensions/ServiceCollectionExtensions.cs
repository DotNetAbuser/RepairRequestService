using Client.Infrastructure.Handlers;

namespace Client.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddManagers(this IServiceCollection services)
    {
        return services
            .AddTransient<IAccountManager, AccountManager>()
            .AddTransient<IEquipmentTypeManager, EquipmentTypeManager>()
            .AddTransient<IProblemTypeManager, ProblemTypeManager>()
            .AddTransient<IRepairStatusManager, RepairStatusManager>()
            .AddTransient<IRequestManager, RequestManager>()
            .AddTransient<ITokenManager, TokenManager>()
            .AddTransient<IUserManager, UserManager>()
            .AddTransient<IRequestManager, RequestManager>();
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddSettingHttp(this IServiceCollection services)
    {
        var apiUri = string.Empty;
#if DEBUG
        apiUri = "http://localhost:5001/";
#else
        apiUri = "http://95.163.233.157:8080/";
#endif
        services
            .AddTransient<AuthenticationHeaderHandler>()
            .AddHttpClient("Client")
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(apiUri);
            })
            .AddHttpMessageHandler<AuthenticationHeaderHandler>();
        return services;
    }
}