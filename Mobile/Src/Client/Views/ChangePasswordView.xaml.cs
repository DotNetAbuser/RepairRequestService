using Client.ViewModels;

namespace Client.Views;

public partial class ChangePasswordView : ContentPage
{
    public ChangePasswordView(ChangePasswordVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}