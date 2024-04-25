using Client.ViewModels;

namespace Client.Views;

public partial class EditRequestView : ContentPage
{
    public EditRequestView(EditRequestVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}