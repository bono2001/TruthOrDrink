using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SQLitePCL;

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

            // Initialiseer SQLite om compatibiliteitsproblemen te voorkomen
            Batteries_V2.Init();

            // Databasepad instellen en repository registreren
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "TruthOrDrink.db");
            Console.WriteLine($"[DOTNET] Databasepad: {dbPath}");

            // Voeg de QuestionRepository toe aan de DI-container
            builder.Services.AddSingleton<QuestionRepository>(provider => new QuestionRepository(dbPath));

            // Registreer QuestionPage en andere pagina's
            builder.Services.AddTransient<QuestionPage>();
            builder.Services.AddTransient<PlayerPage>();
            builder.Services.AddTransient<GameSetting>();
            builder.Services.AddTransient<GamePage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
//deze versie werk