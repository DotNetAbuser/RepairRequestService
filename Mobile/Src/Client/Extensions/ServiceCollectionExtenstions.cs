using Client.Services;
using Client.ViewModels;
using Client.Views;

namespace Client.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddHttpClientForMobile(this IServiceCollection services)
    {
        var apiUri = string.Empty;
#if DEBUG
        apiUri = "http://10.0.2.2:5001/";
#else
        apiUri = "http://89.108.77.44:8080/";
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

    internal static IServiceCollection AddClientServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IAlertService, AlertService>()
            .AddTransient<INavigationService, NavigationService>();
    }
    
    internal static IServiceCollection AddViews(this IServiceCollection services)
    {
        return services
            .AddTransient<HomeView>()
            .AddTransient<ProfileView>()
            .AddTransient<SignInView>()
            .AddTransient<SignUpView>()
            .AddTransient<StartUpView>()
            .AddTransient<UpdateProfileView>()
            .AddTransient<ChangePasswordView>()
            .AddTransient<RequestsView>()
            .AddTransient<CreateRequestView>()
            .AddTransient<RequestDetailsView>()
            .AddTransient<EditRequestView>();
    }
    
    internal static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        return services
            .AddTransient<HomeVM>()
            .AddTransient<ProfileVM>()
            .AddTransient<SignInVM>()
            .AddTransient<SignUpVM>()
            .AddTransient<StartUpVM>()
            .AddTransient<UpdateProfileVM>()
            .AddTransient<ChangePasswordVM>()
            .AddTransient<RequestsVM>()
            .AddTransient<CreateRequestVM>()
            .AddTransient<RequestDetailsVM>()
            .AddTransient<EditRequestVM>();
    }

}