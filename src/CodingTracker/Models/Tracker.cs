using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingTracker.Models;

[Table("Tracker")]
public class Tracker
{
    [Key]
    public required int Id { get; set; }
    public required string Habit { get; set; }
    public required DateTime Date { get; set; }
    public required int Count { get; set; }
}
