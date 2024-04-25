using Client.ViewModels;

namespace Client.Views;

public partial class StartUpView : ContentPage
{
    public StartUpView(StartUpVM vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}