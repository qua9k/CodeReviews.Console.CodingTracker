namespace CodingTracker.Models;

class Tracker
{
    public required int Id { get; set; }
    public required string Habit { get; set; }
    public required int Date { get; set; }
    public required int Count { get; set; }
}
