using Microsoft.Extensions.Logging;
using ECommerceApp.Views;
using ECommerceApp.ViewModels;
using ECommerceApp.Services;

namespace ECommerceApp
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
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons");
                });

            // Register Services
            builder.Services.AddSingleton<DatabaseService>();

            // Register ViewModels
            builder.Services.AddSingleton<ProductViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddSingleton<CartViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();

            // Register Pages
            builder.Services.AddSingleton<MainPage>();
            
            builder.Services.AddSingleton<CartPage>();

            builder.Services.AddTransient<LoginPage>();
            
            builder.Services.AddTransient<RegisterPage>();
           

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
