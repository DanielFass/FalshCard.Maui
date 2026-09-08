using FlashCard.Maui.Data;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace FlashCard.Maui
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
                    fonts.AddFont("Oswald-VariableFont_wght.ttf", "Oswald");
                });
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "game.db");

            builder.Services.AddSingleton(s =>
                ActivatorUtilities.CreateInstance<GameRepository>(s, dbPath));

            builder.Logging.AddDebug();

            return builder.Build();
        }
    }
}