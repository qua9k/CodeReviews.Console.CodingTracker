using CodingTracker.Models;
using CodingTracker.Views;
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

        var sql =
            @"
                CREATE TABLE Tracker (
                    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    Habit TEXT NOT NULL,
                    Date DATE NOT NULL,
                    Count INTEGER NOT NULL
                )
            ";

        connection.Query(sql);

        UserInterface.Pause();
    }

    public void SeedDatabase()
    {
        using SqliteConnection connection = new() { ConnectionString = _connString };

        var seedTrackers = new List<Tracker>
        {
            new()
            {
                Id = 0,
                Habit = "Climbing",
                Date = DateTime.Now,
                Count = 0,
            },
            new()
            {
                Id = 1,
                Habit = "Coding",
                Date = DateTime.Now,
                Count = 1,
            },
            new()
            {
                Id = 2,
                Habit = "Cooking",
                Date = DateTime.Now,
                Count = 2,
            },
        };

        var sql =
            @"
              INSERT INTO Tracker (Id, Habit, Date, Count)
              VALUES (@Id, @Habit, @Date, @Count)
            ";

        connection.Execute(sql, seedTrackers);
    }

    public static void CloseConnection()
    {
        Console.WriteLine("Goodbye.");
    }

    public static bool EntryExists(string connectionString, string primaryKey)
    {
        var sql = $"SELECT * FROM Tracker";

        if (primaryKey != "*")
        {
            sql += $" WHERE id = {primaryKey}";
        }

        using SqliteConnection connection = new() { ConnectionString = connectionString };

        List<Tracker> results = [.. connection.Query<Tracker>(sql)];

        return results.Count > 0;
    }
}
