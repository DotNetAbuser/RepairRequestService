using System.Collections;
using System.Windows.Input;

namespace Client.CustomControls;

public partial class CustomPicker : Border
{
    public CustomPicker()
    {
        InitializeComponent();
    }
    
    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
        propertyName: nameof(SelectedItem),
        returnType: typeof(object),
        declaringType: typeof(CustomPicker),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay);
    
    public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create(
        propertyName: nameof(ItemSource),
        returnType: typeof(IEnumerable),
        declaringType: typeof(CustomPicker),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);
    
    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
        propertyName: nameof(ImageSource),
        returnType: typeof(string),
        declaringType: typeof(CustomPicker),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);
    
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
        propertyName: nameof(Placeholder),
        returnType: typeof(string),
        declaringType: typeof(CustomPicker),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);
    
    public static readonly BindableProperty OnSelectedItemCommandProperty = BindableProperty.Create(
        propertyName: nameof(OnSelectedItemCommand),
        returnType: typeof(ICommand),
        declaringType: typeof(CustomPicker));
    
    public object SelectedItem
    {
        get => (object)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
    
    public IEnumerable ItemSource
    {
        get => (IEnumerable)GetValue(ItemSourceProperty);
        set => SetValue(ItemSourceProperty, value);
    }
    
    public string ImageSource
    {
        get => (string)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
    
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }
    
    public ICommand OnSelectedItemCommand
    {
        get => (ICommand)GetValue(OnSelectedItemCommandProperty);
        set => SetValue(OnSelectedItemCommandProperty, value);
    }
}