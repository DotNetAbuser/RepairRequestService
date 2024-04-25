namespace Client;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		AddRoutes();
	}

	private void AddRoutes()
	{
		Routing.RegisterRoute(nameof(HomeView),
			typeof(HomeView));
		Routing.RegisterRoute(nameof(ProfileView),
			typeof(ProfileView));
		Routing.RegisterRoute(nameof(SignInView),
			typeof(SignInView));
		Routing.RegisterRoute(nameof(SignUpView),
			typeof(SignUpView));
		Routing.RegisterRoute(nameof(StartUpView),
			typeof(StartUpView));
		Routing.RegisterRoute(nameof(UpdateProfileView),
			typeof(UpdateProfileView));
		Routing.RegisterRoute(nameof(ChangePasswordView),
			typeof(ChangePasswordView));
		Routing.RegisterRoute(nameof(CreateRequestView),
			typeof(CreateRequestView));
		Routing.RegisterRoute(nameof(RequestDetailsView),
			typeof(RequestDetailsView));
		Routing.RegisterRoute(nameof(EditRequestView),
			typeof(EditRequestView));
	}
}
