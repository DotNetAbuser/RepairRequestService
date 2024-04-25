namespace Client.Services;

public interface INavigationService
{
    Task NavigateToAsync(string route, IDictionary<string, object> parameters = null);
    Task NavigateBackAsync();
}

public class NavigationService : INavigationService
{
    public Task NavigateToAsync(string route, IDictionary<string, object> parameters = null)
    {
        if (parameters != null)
            return Shell.Current.GoToAsync(route, true, parameters);
        else
            return Shell.Current.GoToAsync(route, true);
    }

    public async Task NavigateBackAsync() =>
        await Shell.Current.GoToAsync("..", true);
}