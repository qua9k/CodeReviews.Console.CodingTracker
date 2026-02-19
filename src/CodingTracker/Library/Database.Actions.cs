using CodingTracker.Models;
using CodingTracker.Views;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CodingTracker.Library;

interface IDbActions
{
    void CreateEntry(string connectionString);
    void ReadEntry(string connectionString);
    void UpdateEntry(string connectionString);
    void DeleteEntry(string connectionString);
}

public partial class Database : IDbActions
{
    public void CreateEntry(string connectionString)
    {
        var habit = UserInterface.PromptForHabit();
        var date = UserInterface.PromptForDate();
        var count = UserInterface.PromptForCount();
        var seedCommand =
            $@" INSERT INTO tracker(date, habit, count) VALUES ('{date}', '{habit}', {count})";

        using SqliteConnection connection = new() { ConnectionString = connectionString };

        connection.Query(seedCommand);

        UserInterface.Pause();
    }

    public void ReadEntry(string connectionString)
    {
        var primaryKey = UserInterface.PromptForId();
        var query = "SELECT * FROM Tracker";

        if (primaryKey != "*")
        {
            query += $" WHERE id = {primaryKey}";
        }

        using SqliteConnection connection = new() { ConnectionString = connectionString };

        List<Tracker> results = [.. connection.Query<Tracker>(query)];

        Console.WriteLine("Your query results:\n");

        if (results.Count < 1)
        {
            Console.WriteLine($"Your query returned no results.");
        }
        else
        {
            foreach (var entry in results)
            {
                Console.WriteLine(
                    $"{entry.Id}.) [{entry.Date:MMM}. {entry.Date.Day}, {entry.Date.Year}] {entry.Habit} x{entry.Count}"
                );
            }
        }

        UserInterface.Pause();
    }

    public void UpdateEntry(string connectionString)
    {
        var primaryKey = UserInterface.PromptForId();

        if (!EntryExists(connectionString, primaryKey))
        {
            Console.WriteLine($"That entry does not exist.");
            UserInterface.Pause();
            return;
        }

        var habit = UserInterface.PromptForHabit();
        var date = UserInterface.PromptForDate();
        var count = UserInterface.PromptForCount();

        var updateCommand =
            @$"
                UPDATE tracker
                SET date = '{date}',
                    habit = '{habit}',
                    count = '{count}'
                WHERE
                    id = {primaryKey}
            ";

        using SqliteConnection connection = new() { ConnectionString = connectionString };

        connection.Query(updateCommand);

        UserInterface.Pause();
    }

    public void DeleteEntry(string connectionString)
    {
        var primaryKey = UserInterface.PromptForId();

        if (!EntryExists(connectionString, primaryKey))
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

        using SqliteConnection connection = new() { ConnectionString = connectionString };

        connection.Query(deleteCommand);

        UserInterface.Pause();
    }
}
