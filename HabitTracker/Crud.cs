using HabitTracker;
using Microsoft.Data.Sqlite;

class Crud : ICrudController
{
    public static void CreateEntry(SqliteConnection connection)
    {
        Console.Clear();
        Console.Write("Enter the date (YYYY-mm-dd): ");

        string? date = Console.ReadLine();
        date = Validator.ValidateField("date", date);

        Console.Write("Enter the habit count: ");

        string? count = Console.ReadLine();
        count = Validator.ValidateField("count", count);

        var seedCommand = connection.CreateCommand();

        seedCommand.CommandText =
            $@"
              INSERT INTO tracker(date, count)
              VALUES 
              ('{date}', {count})
            ";

        seedCommand.ExecuteNonQuery();

        UserInterface.Pause();
    }

    // [[todo]] :: implement
    // [[bug]] ::
    public static void UpdateEntry(SqliteConnection connection)
    {
        string? primaryKey = PromptForId(CrudOps.Update);
        var updateCommand = connection.CreateCommand();

        DateTime newDate = DateTime.Now;
        int newCount = 99;

        Console.WriteLine($"the date: {newDate}");

        updateCommand.CommandText =
            @$"
                UPDATE tracker
                SET date = '{newDate}',
                    count = {newCount}
                WHERE
                    id = {primaryKey}
            ";

        UserInterface.Pause();
    }

    public static void DeleteEntry(SqliteConnection connection)
    {
        string? primaryKey = PromptForId(CrudOps.Delete);
        var deleteCommand = connection.CreateCommand();

        deleteCommand.CommandText = $"DELETE FROM tracker";

        if (primaryKey != "*")
        {
            deleteCommand.CommandText += $" WHERE id = {primaryKey}";
        }

        deleteCommand.ExecuteNonQuery();

        UserInterface.Pause();
    }

    public static string PromptForId(string crudOp)
    {
        string message = $"Enter the id of the entry to {crudOp}";

        Console.Clear();

        if (crudOp == CrudOps.Update)
        {
            Console.Write($"{message}: ");
        }
        else
        {
            Console.Write($"{message} ('*' for all entries): ");
        }

        string? id = Console.ReadLine();

        if (id == "*" && (crudOp == CrudOps.Delete || crudOp == CrudOps.Read))
        {
            return id;
        }

        id = Validator.ValidateField(TableFields.Id, id);

        return id;
    }

    public static void ReadEntry(SqliteConnection connection)
    {
        string? primaryKey = PromptForId(CrudOps.Read);
        var selectCommand = connection.CreateCommand();

        selectCommand.CommandText = "SELECT * FROM tracker";

        if (primaryKey != "*")
        {
            selectCommand.CommandText += $" WHERE id = {primaryKey}";
        }

        using var reader = selectCommand.ExecuteReader();

        if (!reader.HasRows)
        {
            Console.Clear();
            Console.WriteLine($"Your query returned no results.");
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Your query results:\n");

            while (reader.Read())
            {
                var id = reader.GetInt16(0);
                var date = reader.GetDateTime(1);
                var count = reader.GetInt16(2);

                Console.WriteLine($"{id}.) {date:MMM}. {date.Day}, {date.Year}: {count}");
            }
        }

        UserInterface.Pause();
    }
}
