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

    public void CreateDatabase()
    {
        Console.WriteLine("Creating database...\nDatabase created.");

        var sql =
            @"
                CREATE TABLE Tracker (
                    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    Date DATE NOT NULL,
                    StartTime DATE NOT NULL,
                    EndTime DATE NOT NULL,
                    Duration DATE NOT NULL
                )
            ";

        using SqliteConnection connection = new() { ConnectionString = _connString };

        connection.Query(sql);

        UserInterface.Pause();
    }

    public void SeedDatabase()
    {
        using SqliteConnection connection = new() { ConnectionString = _connString };

        var seedTrackers = new List<CodingSession>
        {
            new()
            {
                Id = 0,
                Date = DateTime.Now,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
            },
            new()
            {
                Id = 1,
                Date = DateTime.Now,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
            },
        };

        var sql =
            @"
              INSERT INTO Tracker (Id, Date, StartTime, EndTime, Duration)
              VALUES (@Id, @Date, @StartTime, @EndTime, @Duration)
            ";

        connection.Execute(sql, seedTrackers);
    }

    public static void CloseConnection()
    {
        Console.WriteLine("Goodbye.");
    }

    public bool EntryExists(string primaryKey)
    {
        var sql = $"SELECT * FROM Tracker";

        if (primaryKey != "*")
        {
            sql += $" WHERE id = {primaryKey}";
        }

        using SqliteConnection connection = new() { ConnectionString = _connString };

        List<CodingSession> results = [.. connection.Query<CodingSession>(sql)];

        return results.Count > 0;
    }
}
