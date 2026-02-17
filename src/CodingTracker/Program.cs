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
        string connectionString = $"Data Source={dbPath}";
        Database dBase = new(dbPath);

        if (!File.Exists(dbPath))
        {
            dBase.CreateDatabase();
            dBase.SeedDatabase();
        }

        bool connected = true;

        while (connected)
        {
            UserInterface.PrintMenuOptions();

            var input = Console.ReadLine();

            switch (input)
            {
                case "c":
                    Crud.CreateEntry(connectionString);
                    break;
                case "r":
                    Crud.ReadEntry(connectionString);
                    break;
                case "u":
                    Crud.UpdateEntry(connectionString);
                    break;
                case "d":
                    Crud.DeleteEntry(connectionString);
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
