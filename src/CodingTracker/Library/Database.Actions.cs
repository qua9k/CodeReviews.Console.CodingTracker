using CodingTracker.Models;
using CodingTracker.Views;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CodingTracker.Library;

interface IDbActions
{
    void CreateEntry();
    void ReadEntry();
    void UpdateEntry();
    void DeleteEntry();
}

public partial class Database : IDbActions
{
    public void CreateEntry()
    {
        CodingSession newSession = new()
        {
            Id = 3,
            Date = DateTime.Now,
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
        };

        var sql =
            @"
              INSERT INTO Tracker (Id, Date, StartTime, EndTime, Duration)
              VALUES (@Id, @Date, @StartTime, @EndTime, @Duration)
            ";

        using SqliteConnection connection = new() { ConnectionString = _connString };

        connection.Execute(sql, newSession);

        UserInterface.Pause();
    }

    public void ReadEntry()
    {
        var primaryKey = UserInterface.PromptForId();
        var query = "SELECT * FROM Tracker";

        if (primaryKey != "*")
        {
            query += $" WHERE id = {primaryKey}";
        }

        using SqliteConnection connection = new() { ConnectionString = _connString };

        List<CodingSession> results = [.. connection.Query<CodingSession>(query)];

        Console.WriteLine("\nYour query results:\n");

        if (results.Count < 1)
        {
            Console.WriteLine($"Your query returned no results.");
        }
        else
        {
            foreach (var entry in results)
            {
                Console.WriteLine(
                    $"{entry.Id}.) [{entry.Date:MMM}. {entry.Date.Day}, {entry.Date.Year}] {entry.StartTime} {entry.EndTime} {entry.Duration}"
                );
            }
        }

        UserInterface.Pause();
    }

    public void UpdateEntry()
    {
        var primaryKey = UserInterface.PromptForId();

        if (!EntryExists(_connString, primaryKey))
        {
            Console.WriteLine($"That entry does not exist.");
            UserInterface.Pause();
            return;
        }

        // [[todo]] :: must update
        //     [[bug]] ::
        CodingSession newSession = new()
        {
            Id = 0,
            Date = new DateTime(9999, 01, 01),
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
        };

        var updateCommand =
            @$"
                UPDATE tracker
                SET Date = @Date,
                    StartTime = @StartTime,
                    EndTime = @EndTime
                WHERE 
                    id = @Id
            ";

        using SqliteConnection connection = new() { ConnectionString = _connString };

        connection.Execute(updateCommand, newSession);

        UserInterface.Pause();
    }

    public void DeleteEntry()
    {
        var primaryKey = UserInterface.PromptForId();

        if (!EntryExists(_connString, primaryKey))
        {
            Console.WriteLine($"That entry does not exist.");
            UserInterface.Pause();
            return;
        }

        var deleteCommand = $"DELETE FROM Tracker";

        if (primaryKey != "*")
        {
            deleteCommand += $" WHERE id = {primaryKey}";
        }

        using SqliteConnection connection = new() { ConnectionString = _connString };

        connection.Execute(deleteCommand);

        UserInterface.Pause();
    }
}
