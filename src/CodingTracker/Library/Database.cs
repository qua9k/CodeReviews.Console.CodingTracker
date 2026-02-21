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
                    Date TEXT NOT NULL,
                    StartTime TEXT NOT NULL,
                    EndTime TEXT NOT NULL,
                    Duration TEXT 
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
                Date = "1999-01-01",
                StartTime = "13:00",
                EndTime = "13:41",
            },
            new()
            {
                Date = "1999-02-02",
                StartTime = "14:00",
                EndTime = "13:00",
            },
            new()
            {
                Date = "1999-03-03",
                StartTime = "16:40",
                EndTime = "19:00",
            },
        };

        var sql =
            @"
              INSERT INTO Tracker (Date, StartTime, EndTime)
              VALUES (@Date, @StartTime, @EndTime)
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
