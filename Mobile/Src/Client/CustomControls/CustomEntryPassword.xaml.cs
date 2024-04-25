namespace Client.CustomControls;

public partial class CustomEntryPassword : Border
{
    public CustomEntryPassword()
    {
        InitializeComponent();
    }
    
    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
        propertyName: nameof(ImageSource),
        returnType: typeof(string),
        declaringType: typeof(CustomEntryPassword),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);
    
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        propertyName: nameof(Text),
        returnType: typeof(string),
        declaringType: typeof(CustomEntryPassword),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay);
    
    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(
        propertyName: nameof(Placeholder),
        returnType: typeof(string),
        declaringType: typeof(CustomEntryPassword),
        defaultValue: "Поле",
        defaultBindingMode: BindingMode.OneWay);
    
    public string ImageSource
    {
        get => (string)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
    
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }
}