using CodingTracker.Models;
using Dapper;
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

        using var connection = new SqliteConnection();

        if (!File.Exists(dbPath))
        {
            connection.ConnectionString = $"Data Source={dbPath}";
            connection.Open();
            Database.CreateDatabase(connection);
            Database.SeedDatabase(connection);
        }
        else
        {
            connection.ConnectionString = $"Data Source={dbPath}";
            connection.Open();
        }

        bool connected = true;

        while (connected)
        {
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
                    connected = Database.CloseConnection(connection);
                    break;
                default:
                    UserInterface.PrintInputUnknown();
                    break;
            }

            Console.WriteLine();
        }
    }
}
