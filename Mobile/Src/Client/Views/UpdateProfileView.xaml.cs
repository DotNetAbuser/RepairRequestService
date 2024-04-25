namespace Client.Views;

public partial class UpdateProfileView : ContentPage
{
    public UpdateProfileView(UpdateProfileVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}