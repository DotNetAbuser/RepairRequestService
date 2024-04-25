
using Client.Services;
#if ANDROID
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif

namespace Client;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.UseBottomSheet()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("MaterialIconsOutlined-Regular.otf", "MaterialIconsOutlined-Regular");
				fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons-Regular");
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		
		builder.Services.AddViews();
		builder.Services.AddViewModels();
		builder.Services.AddManagers();
		builder.Services.AddClientServices();
		builder.Services.AddTransient<IStorageService, StorageService>();
		builder.Services.AddHttpClientForMobile();
		
#if DEBUG
		builder.Logging.AddDebug();
#endif
		
#if ANDROID
		// Remove Entry control underline
		Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
		{
			h.PlatformView.BackgroundTintList =
				Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
		});
		// Remove Picker control underline
		Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
		{
			h.PlatformView.BackgroundTintList =
				Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
		});

		// Remove Editor control underline
		Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
		{
			h.PlatformView.BackgroundTintList =
				Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
		});

		// Remove SearchBar control underline
		Microsoft.Maui.Handlers.SearchBarHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
		{
			h.PlatformView.BackgroundTintList =
				Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
		});
#endif
		return builder.Build();
	}
}
