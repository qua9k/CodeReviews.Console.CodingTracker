using Dapper;
using Microsoft.Data.Sqlite;

namespace CodingTracker.Library;

public class Database(string databaseFilePath)
{
    private readonly string _connectionString = databaseFilePath;

    public void CreateDatabase()
    {
        using SqliteConnection connection = new()
        {
            ConnectionString = $"Data Source={_connectionString}",
        };

        Console.WriteLine("Creating database...\nDatabase created.");

        var operation =
            @"
                CREATE TABLE Tracker (
                    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    date DATE NOT NULL,
                    habit TEXT NOT NULL,
                    count INTEGER NOT NULL
                )
            ";

        connection.Query(operation);

        UserInterface.Pause();
    }

    public void SeedDatabase()
    {
        using SqliteConnection connection = new()
        {
            ConnectionString = $"Data Source={_connectionString}",
        };

        var operation =
            @"
              INSERT INTO Tracker
              VALUES 
              (1, '1901-01-01', 'Cooking', 1),
              (2, '1902-02-02', 'Cleaning', 2),
              (3, '1903-03-03', 'Drumming', 3)
            ";

        connection.Query(operation);
    }

    public static void CloseConnection()
    {
        Console.WriteLine("Goodbye.");
    }
}
