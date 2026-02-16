namespace CodingTracker.Models;

interface ITrackerFields
{
    internal const string Id = "id";
    internal const string Habit = "habit";
    internal const string Date = "date";
    internal const string Count = "count";
}

public class Tracker : ITrackerFields
{
    public required int Id { get; set; }
    public required string Habit { get; set; }
    public required DateTime Date { get; set; }
    public required int Count { get; set; }
}
