using CodingTracker.Models;
using CodingTracker.Views;
using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;

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
        var date = UserInterface.PromptForDate();
        var startTime = UserInterface.PromptForStartTime();
        var endTime = UserInterface.PromptForEndTime();

        CodingSession entry = new()
        {
            Date = date,
            StartTime = startTime,
            EndTime = endTime,
        };

        var sql =
            @"
              INSERT INTO Tracker (Date, StartTime, EndTime)
              VALUES (@Date, @StartTime, @EndTime)
            ";

        using SqliteConnection connection = new() { ConnectionString = _connString };

        connection.Execute(sql, entry);

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
            Table table = new();
            table.AddColumns(["Id", "Date", "Start Time", "End Time", "Duration"]);

            foreach (var entry in results)
            {
                var id = Convert.ToString(entry.Id);
                var date = Convert.ToDateTime(entry.Date).ToString("d");
                var startTime = Convert.ToDateTime(entry.StartTime).ToString("t");
                var endTime = Convert.ToDateTime(entry.EndTime).ToString("t");
                var duration = Convert.ToString(entry.Duration) + " minutes";

                table.AddRow(id, date, startTime, endTime, duration);
            }

            AnsiConsole.Write(table);
        }

        UserInterface.Pause();
    }

    public void UpdateEntry()
    {
        var primaryKey = UserInterface.PromptForId();

        if (!EntryExists(primaryKey))
        {
            Console.WriteLine($"That entry does not exist.");
            UserInterface.Pause();
            return;
        }

        var date = UserInterface.PromptForDate();
        var startTime = UserInterface.PromptForStartTime();
        var endTime = UserInterface.PromptForEndTime();

        CodingSession updatedEntry = new()
        {
            Date = date,
            StartTime = startTime,
            EndTime = endTime,
        };

        var updateCommand =
            @$"
                UPDATE tracker
                SET Date = @Date,
                    StartTime = @StartTime,
                    EndTime = @EndTime
                WHERE 
                    id = {primaryKey}
            ";

        using SqliteConnection connection = new() { ConnectionString = _connString };

        connection.Execute(updateCommand, updatedEntry);

        UserInterface.Pause();
    }

    public void DeleteEntry()
    {
        var primaryKey = UserInterface.PromptForId();

        if (!EntryExists(primaryKey))
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
