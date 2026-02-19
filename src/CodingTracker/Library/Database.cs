using CodingTracker.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CodingTracker.Library;

public partial class Database
{
    private readonly string _dbFilePath;
    private readonly string _connString;

    public Database(string dbFilePath)
    {
        _dbFilePath = dbFilePath;
        _connString = $"Data Source={_dbFilePath}";

        if (!File.Exists(dbFilePath))
        {
            CreateDatabase();
            SeedDatabase();
        }
    }

    public string GetConnectionString()
    {
        return _connString;
    }

    public void CreateDatabase()
    {
        using SqliteConnection connection = new() { ConnectionString = _connString };

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
        using SqliteConnection connection = new() { ConnectionString = _connString };

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

    public void CloseConnection()
    {
        Console.WriteLine("Goodbye.");
    }

    public static bool EntryExists(string connectionString, string primaryKey)
    {
        using SqliteConnection connection = new() { ConnectionString = connectionString };

        List<Tracker> results =
        [
            .. connection.Query<Tracker>($"SELECT * FROM tracker WHERE id = {primaryKey}"),
        ];

        return results.Count > 0;
    }
}
