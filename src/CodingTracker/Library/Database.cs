using Microsoft.Data.Sqlite;

namespace CodingTracker.Library;

// [[todo]] :: convert to dapper
public class Database
{
    public static void CreateDatabase(SqliteConnection connection)
    {
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

        var command = connection.CreateCommand();
        command.CommandText = operation;
        command.ExecuteNonQuery();
        UserInterface.Pause();
    }

    public static void SeedDatabase(SqliteConnection connection)
    {
        var seedCommand = connection.CreateCommand();

        var operation =
            @"
              INSERT INTO Tracker
              VALUES 
              (1, '1901-01-01', 'Rock Climbing', 1),
              (2, '1902-02-02', 'Guitar', 2),
              (3, '1903-03-03', 'Painting', 3)
            ";

        seedCommand.CommandText = operation;
        seedCommand.ExecuteNonQuery();
    }

    public static void CloseConnection(SqliteConnection connection)
    {
        Console.WriteLine("Goodbye.");
        connection.Close();
    }
}
