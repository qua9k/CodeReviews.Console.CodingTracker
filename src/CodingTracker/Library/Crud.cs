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
    static abstract void CreateEntry(SqliteConnection connection);
    static abstract void ReadEntry(SqliteConnection connection);
    static abstract void UpdateEntry(SqliteConnection connection);
    static abstract void DeleteEntry(SqliteConnection connection);
}

class Crud
{
    public static void CreateEntry(SqliteConnection connection)
    {
        Console.Clear();

        var habit = PromptForHabit();
        var date = PromptForDate();
        var count = PromptForCount();

        var seedCommand = connection.CreateCommand();

        seedCommand.CommandText =
            $@"
              INSERT INTO tracker(date, habit, count)
              VALUES 
              ('{date}', '{habit}', {count})
            ";

        seedCommand.ExecuteNonQuery();

        UserInterface.Pause();
    }

    public static void UpdateEntry(SqliteConnection connection)
    {
        var primaryKey = PromptForId(CrudOperations.Update);

        if (!EntryExists(connection, primaryKey))
        {
            Console.WriteLine($"That entry does not exist.");
            UserInterface.Pause();
            return;
        }

        var updateCommand = connection.CreateCommand();
        var habit = PromptForHabit();
        var date = PromptForDate();
        var count = PromptForCount();

        updateCommand.CommandText =
            @$"
                UPDATE tracker
                SET date = '{date}',
                    habit = '{habit}',
                    count = '{count}'
                WHERE
                    id = {primaryKey}
            ";

        updateCommand.ExecuteNonQuery();

        UserInterface.Pause();
    }

    public static void DeleteEntry(SqliteConnection connection)
    {
        var primaryKey = PromptForId(CrudOperations.Delete);
        var deleteCommand = connection.CreateCommand();

        deleteCommand.CommandText = $"DELETE FROM tracker";

        if (primaryKey != "*")
        {
            deleteCommand.CommandText += $" WHERE id = {primaryKey}";
        }

        deleteCommand.ExecuteNonQuery();

        UserInterface.Pause();
    }

    public static string PromptForHabit()
    {
        Console.Write("Enter the habit: ");
        var habit = Console.ReadLine();
        habit = Validator.ValidateField("habit", habit);
        return habit;
    }

    public static string PromptForCount()
    {
        Console.Write("Enter the count: ");
        var count = Console.ReadLine();
        count = Validator.ValidateField("count", count);
        return count;
    }

    public static string PromptForDate()
    {
        Console.Write("Enter the date (YYYY-MM-DD or 't' for today): ");

        var date = Console.ReadLine();

        if (date == "t")
        {
            date = Convert.ToString(DateTime.Today);
        }

        date = Validator.ValidateField("date", date);

        return date;
    }

    public static string PromptForId(string crudOp)
    {
        var message = $"Enter the id of the entry to {crudOp}";

        Console.Clear();

        if (crudOp == CrudOperations.Update)
        {
            Console.Write($"{message}: ");
        }
        else
        {
            Console.Write($"{message} ('*' for all results): ");
        }

        var id = Console.ReadLine();

        if (id == "*" && (crudOp == CrudOperations.Delete || crudOp == CrudOperations.Read))
        {
            return id;
        }

        id = Validator.ValidateField(ITrackerFields.Id, id);

        return id;
    }

    public static bool EntryExists(SqliteConnection connection, string primaryKey)
    {
        List<Tracker> results =
        [
            .. connection.Query<Tracker>($"SELECT * FROM tracker WHERE id = {primaryKey}"),
        ];
        return results.Count > 0;
    }

    public static void ReadEntry(SqliteConnection connection)
    {
        var primaryKey = PromptForId(CrudOperations.Read);
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
}
