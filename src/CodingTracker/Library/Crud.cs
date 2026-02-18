using CodingTracker.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CodingTracker.Library;

static class CrudOperations
{
    internal static readonly string Create = "create";
    internal static readonly string Read = "read";
    internal static readonly string Update = "update";
    internal static readonly string Delete = "delete";
}

interface ICrudActions
{
    static abstract void CreateEntry(string connectionString);
    static abstract void ReadEntry(string connectionString);
    static abstract void UpdateEntry(string connectionString);
    static abstract void DeleteEntry(string connectionString);
}

class Crud : ICrudActions
{
    public static void CreateEntry(string connectionString)
    {
        Console.Clear();

        using SqliteConnection connection = new() { ConnectionString = connectionString };

        var habit = UserInterface.PromptForHabit();
        var date = UserInterface.PromptForDate();
        var count = UserInterface.PromptForCount();
        var seedCommand =
            $@" INSERT INTO tracker(date, habit, count) VALUES ('{date}', '{habit}', {count})";

        connection.Query(seedCommand);

        UserInterface.Pause();
    }

    public static void ReadEntry(string connectionString)
    {
        using SqliteConnection connection = new() { ConnectionString = connectionString };

        var primaryKey = UserInterface.PromptForId(CrudOperations.Read);

        if (primaryKey != "*")
        {
            primaryKey = Validator.ValidateField(ITrackerFields.Id, primaryKey);
        }

        var query = "SELECT * FROM Tracker";

        if (primaryKey != "*")
        {
            query += $" WHERE id = {primaryKey}";
        }

        List<Tracker> results = [.. connection.Query<Tracker>(query)];

        if (results.Count < 1)
        {
            Console.Clear();
            Console.WriteLine($"Your query returned no results.");
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Your query results:\n");

            foreach (var entry in results)
            {
                Console.WriteLine(
                    $"{entry.Id}.) [{entry.Date:MMM}. {entry.Date.Day}, {entry.Date.Year}] {entry.Habit} x{entry.Count}"
                );
            }
        }

        UserInterface.Pause();
    }

    public static void UpdateEntry(string connectionString)
    {
        using SqliteConnection connection = new() { ConnectionString = connectionString };

        var primaryKey = UserInterface.PromptForId(CrudOperations.Read);
        primaryKey = Validator.ValidateField(ITrackerFields.Id, primaryKey);

        if (!EntryExists(connection, primaryKey))
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

        connection.Query(updateCommand);

        UserInterface.Pause();
    }

    public static void DeleteEntry(string connectionString)
    {
        using SqliteConnection connection = new() { ConnectionString = connectionString };
        var primaryKey = UserInterface.PromptForId(CrudOperations.Read);

        if (primaryKey != "*")
        {
            primaryKey = Validator.ValidateField(ITrackerFields.Id, primaryKey);
        }

        var deleteCommand = $"DELETE FROM tracker";

        if (primaryKey != "*")
        {
            deleteCommand += $" WHERE id = {primaryKey}";
        }

        connection.Query(deleteCommand);

        UserInterface.Pause();
    }

    public static bool EntryExists(SqliteConnection connection, string primaryKey)
    {
        List<Tracker> results =
        [
            .. connection.Query<Tracker>($"SELECT * FROM tracker WHERE id = {primaryKey}"),
        ];

        return results.Count > 0;
    }
}
