using Microsoft.Data.Sqlite;

namespace CodingTracker.Controllers;

interface ICrudController
{
    static abstract void CreateEntry(SqliteConnection connection);
    static abstract void ReadEntry(SqliteConnection connection);
    static abstract void UpdateEntry(SqliteConnection connection);
    static abstract void DeleteEntry(SqliteConnection connection);
}
