namespace CodingTracker.Library;

// You'll need to create a CodingSession class in a separate file.
// It will contain the properties of your coding session: Id, StartTime, EndTime, Duration.
// When reading from the database, you can't use an anonymous object, you have to read your table into a List of CodingSession.

interface ICodingSessions { }

class CodingSession
{
    private int _id { get; set; }
    private DateTime _startTime { get; set; } // should be able to enter manual
    private DateTime _endTime { get; set; } // should be able to enter manual
    private int _duration { get; set; } // calculated based on start/end time
}
