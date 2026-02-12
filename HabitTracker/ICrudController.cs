using Microsoft.Data.Sqlite;

namespace HabitTracker;

interface ICrudController
{
    void CreateEntry(SqliteConnection connection);
    void ReadEntry(SqliteConnection connection);
    void UpdateEntry(SqliteConnection connection);
    void DeleteEntry(SqliteConnection connection);
}
