using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingTracker.Models;

[Table("Tracker")]
class CodingSession
{
    [Key]
    public required int Id { get; set; }
    public required DateTime Date { get; set; } // should be able to enter manual
    public required DateTime StartTime { get; set; } // should be able to enter manual
    public required DateTime EndTime { get; set; } // should be able to enter manual
    public required DateTime Duration { get; set; } // calculated based on start/end time
}
