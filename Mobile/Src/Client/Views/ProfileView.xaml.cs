using Client.ViewModels;

namespace Client.Views;

public partial class ProfileView : ContentPage
{
    public ProfileView(ProfileVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}