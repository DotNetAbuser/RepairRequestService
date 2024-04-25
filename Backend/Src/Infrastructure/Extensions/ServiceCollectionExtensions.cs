using Infrastructure.Services;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHelpers(this IServiceCollection services)
    {
        return services
            .AddTransient<IPasswordHasher, PasswordHasher>();
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddTransient<IEquipmentTypeRepository, EquipmentTypeRepository>()
            .AddTransient<IProblemTypeRepository, ProblemTypeRepository>()
            .AddTransient<IRefreshSessionRepository, RefreshSessionRepository>()
            .AddTransient<IRepairRequestRepository, RepairRequestRepository>()
            .AddTransient<IRepairStatusRepository, RepairStatusRepository>()
            .AddTransient<IRoleRepository, RoleRepository>()
            .AddTransient<IUserRepository, UserRepository>();
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IAccountService, AccountService>()
            .AddTransient<IRepairRequestService, RepairRequestService>()
            .AddTransient<IRoleService, RoleService>()
            .AddTransient<ITokenService, TokenService>()
            .AddTransient<IUserService, UserService>()
            .AddTransient<IEquipmentTypeService, EquipmentTypeService>()
            .AddTransient<IProblemTypeService, ProblemTypeService>()
            .AddTransient<IRepairStatusService, RepairStatusService>();
    }
}