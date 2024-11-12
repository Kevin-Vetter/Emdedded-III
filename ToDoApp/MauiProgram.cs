using Microsoft.Extensions.Logging;
using ToDoApp.Service;
using ToDoApp.Services;
using ToDoApp.ViewModels;
using ToDoApp.Views;

namespace ToDoApp
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
                });
            builder.Services.AddSingleton(Connectivity.Current);

            builder.Services.AddSingleton<IToDoService, MockRepo>();
            builder.Services.AddSingleton<IRestService, RestService>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();

            builder.Services.AddTransient<AddEditToDoViewModel>();
            builder.Services.AddTransient<AddEditToDoPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
