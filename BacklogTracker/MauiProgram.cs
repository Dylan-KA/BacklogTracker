using BacklogTracker.Models;
using BacklogTracker.ViewModels;
using BacklogTracker.Views;
using Microsoft.Extensions.Logging;

namespace BacklogTracker
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
                    fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");
                    fonts.AddFont("Roboto-Bold.ttf", "RobotoBold");
                });

            builder.Services.AddSingleton<App>();
            builder.Services.AddSingleton<LocalDBService>();
            builder.Services.AddSingleton<GameListViewModel>();
            builder.Services.AddSingleton<GameListPage>();

            builder.Services.AddTransient<GameDetailsViewModel>();
            builder.Services.AddTransient<GameDetailsPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
