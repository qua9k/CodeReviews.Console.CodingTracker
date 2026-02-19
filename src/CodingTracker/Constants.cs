namespace CodingTracker;

public static class Constants
{
    public static class CrudOperations
    {
        public const string Create = "create";
        public const string Read = "read";
        public const string Update = "update";
        public const string Delete = "delete";
    }

    // [[note]] :: to be replaced by CodingSession
    public static class TrackerFields
    {
        public const string Id = "id";
        public const string Habit = "habit";
        public const string Date = "date";
        public const string Count = "count";
    }
}
