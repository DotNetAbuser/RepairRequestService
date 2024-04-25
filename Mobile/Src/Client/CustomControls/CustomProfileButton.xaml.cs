using System.Windows.Input;

namespace Client.CustomControls;

public partial class CustomProfileButton : Border
{
    public CustomProfileButton()
    {
        InitializeComponent();
    }
    
    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
        propertyName: nameof(ImageActionSource),
        returnType: typeof(string),
        declaringType: typeof(CustomProfileButton),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);
    
    public static readonly BindableProperty ButtonTitleProperty = BindableProperty.Create(
        propertyName: nameof(ButtonTitle),
        returnType: typeof(string),
        declaringType: typeof(CustomProfileButton),
        defaultValue: "Empty",
        defaultBindingMode: BindingMode.OneWay);
    
    public static readonly BindableProperty ButtonTitleSecondoryProperty = BindableProperty.Create(
        propertyName: nameof(ButtonTitleSecondory),
        returnType: typeof(string),
        declaringType: typeof(CustomProfileButton),
        defaultValue: "Empty",
        defaultBindingMode: BindingMode.OneWay);
    
    public static readonly BindableProperty ButtonCommandProperty = BindableProperty.Create(
        propertyName: nameof(ButtonCommand),
        returnType: typeof(ICommand),
        declaringType: typeof(CustomProfileButton));
    
    public string ImageActionSource
    {
        get => (string)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
    
    public string ButtonTitle
    {
        get => (string)GetValue(ButtonTitleProperty);
        set => SetValue(ButtonTitleProperty, value);
    }
    
    public string ButtonTitleSecondory
    {
        get => (string)GetValue(ButtonTitleSecondoryProperty);
        set => SetValue(ButtonTitleSecondoryProperty, value);
    }

    public ICommand ButtonCommand
    {
        get => (ICommand)GetValue(ButtonCommandProperty);
        set => SetValue(ButtonCommandProperty, value);
    }
    
    
}