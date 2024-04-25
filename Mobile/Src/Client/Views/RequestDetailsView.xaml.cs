using Client.ViewModels;

namespace Client.Views;

public partial class RequestDetailsView : ContentPage
{
    public RequestDetailsView(RequestDetailsVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}