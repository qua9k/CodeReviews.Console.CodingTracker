using Microsoft.Data.Sqlite;

namespace HabitTracker;

class Program
{
    static void Main()
    {
        string databaseFile = "habit.db";
        using var connection = new SqliteConnection();

        if (!File.Exists(databaseFile))
        {
            connection.ConnectionString = $"Data Source={databaseFile}";
            connection.Open();
            Database.CreateDatabase(connection);
            Database.SeedDatabase(connection);
            UserInterface.Pause();
        }
        else
        {
            connection.ConnectionString = $"Data Source={databaseFile}";
            connection.Open();
        }

        bool connected = true;

        while (connected)
        {
            UserInterface.PrintMenuOptions();

            string? input = Console.ReadLine();

            switch (input)
            {
                case "c":
                    Database.CreateEntry(connection);
                    break;
                case "r":
                    Database.ReadEntry(connection);
                    break;
                case "u":
                    Database.UpdateEntry(connection);
                    break;
                case "d":
                    Database.DeleteEntry(connection);
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
