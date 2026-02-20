using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingTracker.Models;

[Table("Tracker")]
public class CodingSession
{
    private string _duration = "a very long time";

    [Key]
    public uint Id { get; set; }
    public required string Date { get; set; }
    public required string StartTime { get; set; }
    public required string EndTime { get; set; }
    public string Duration
    {
        get { return _duration; }
        set { _duration = value; }
    } // [[bug]] :: this is dummy data. must be computed as endtime - starttime.
}
