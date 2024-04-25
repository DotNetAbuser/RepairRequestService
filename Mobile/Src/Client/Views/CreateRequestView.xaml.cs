using Client.ViewModels;

namespace Client.Views;

public partial class CreateRequestView : ContentPage
{
    public CreateRequestView(CreateRequestVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}