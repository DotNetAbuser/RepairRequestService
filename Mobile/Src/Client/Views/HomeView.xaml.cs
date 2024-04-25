using Client.ViewModels;

namespace Client.Views;

public partial class HomeView : ContentPage
{
    public HomeView(HomeVM vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}