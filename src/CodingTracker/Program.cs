using CodingTracker.Library;
using Microsoft.Extensions.Configuration;

namespace CodingTracker;

class Program
{
    static void Main()
    {
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var databaseFilePath = config.GetSection("Settings")["DatabasePath"];
        Database database = new(databaseFilePath!);

        var connected = true;

        while (connected)
        {
            UserInterface.PrintMenuOptions();

            var input = Console.ReadLine();

            switch (input)
            {
                case "c":
                    database.CreateEntry(database.GetConnectionString());
                    break;
                case "r":
                    database.ReadEntry(database.GetConnectionString());
                    break;
                case "u":
                    database.UpdateEntry(database.GetConnectionString());
                    break;
                case "d":
                    database.DeleteEntry(database.GetConnectionString());
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
