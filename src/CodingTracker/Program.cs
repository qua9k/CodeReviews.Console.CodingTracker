using CodingTracker.Library;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CodingTracker;

class Program
{
    static void Main()
    {
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        IConfigurationSection settingsSection = config.GetSection("Settings");
        string? dbPath = settingsSection["DatabasePath"];
        Database dBase = new(dbPath);

        if (!File.Exists(dbPath))
        {
            dBase.CreateDatabase();
            dBase.SeedDatabase();
        }

        bool connected = true;

        while (connected)
        {
            SqliteConnection connection = new() { ConnectionString = $"Data Source={dbPath}" };

            UserInterface.PrintMenuOptions();

            var input = Console.ReadLine();

            switch (input)
            {
                case "c":
                    Crud.CreateEntry(connection);
                    break;
                case "r":
                    Crud.ReadEntry(connection);
                    break;
                case "u":
                    Crud.UpdateEntry(connection);
                    break;
                case "d":
                    Crud.DeleteEntry(connection);
                    break;
                case "x":
                    Database.CloseConnection();
                    connected = false;
                    break;
                default:
                    UserInterface.PrintInputUnknown();
                    break;
            }

            Console.WriteLine();
        }
    }
}
