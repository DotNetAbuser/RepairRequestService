using Client.ViewModels;

namespace Client.Views;

public partial class SignUpView : ContentPage
{
    public SignUpView(SignUpVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}