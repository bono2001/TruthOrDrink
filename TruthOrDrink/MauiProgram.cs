using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace TruthOrDrink
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Registreer de LocalDbService met een databasepad
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "TruthOrDrink.db");
            builder.Services.AddSingleton<QuestionRepository>(provider => new QuestionRepository(dbPath));

            // Registreer ZenQuotesService
            builder.Services.AddSingleton<ZenQuotesService>();

            // Registreer QuestionPage
            builder.Services.AddTransient<QuestionPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
