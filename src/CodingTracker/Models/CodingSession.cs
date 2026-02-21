using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingTracker.Models;

[Table("Tracker")]
public class CodingSession
{
    [Key]
    public uint Id { get; set; }
    public required string Date { get; set; }
    public required string StartTime { get; set; }
    public required string EndTime { get; set; }
    public double Duration
    {
        get
        {
            return Convert.ToDateTime(EndTime).Subtract(Convert.ToDateTime(StartTime)).TotalMinutes;
        }
    }
}
