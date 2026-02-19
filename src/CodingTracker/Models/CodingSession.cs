using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingTracker.Models;

[Table("Tracker")]
public class CodingSession
{
    [Key]
    public required int Id { get; set; }
    public required DateTime Date { get; set; } // should be able to enter manual
    public required DateTime StartTime { get; set; } // should be able to enter manual
    public required DateTime EndTime { get; set; } // should be able to enter manual
    public DateTime Duration { get; set; } // calculated based on start/end time
}
