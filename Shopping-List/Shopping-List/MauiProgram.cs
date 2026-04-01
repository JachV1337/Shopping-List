using Microsoft.Extensions.Logging;
using Shopping_List.Pages;
using Shopping_List.ViewModel;

namespace Shopping_List
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterPages()
                .RegisterViewModels();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var mauiApp = builder.Build();
            App.Services = mauiApp.Services;
            return mauiApp;
        }

        public static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<AddItems>();
            return builder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<ListViewModel>();
            return builder;
        }
    }
}
