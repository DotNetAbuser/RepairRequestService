using Client.ViewModels;

namespace Client.Views;

public partial class RequestsView : ContentPage
{
    public RequestsView(RequestsVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}