using System.Globalization;

namespace CodingTracker.Library;

class InputValidator
{
    public static string ValidateId(string id)
    {
        if (id == "*")
        {
            return id;
        }

        while (!uint.TryParse(id, out uint _))
        {
            Console.Clear();
            Console.WriteLine("The id must be an integer greater than 0.");
            Console.Write($"Please re-enter the id: ");
            id = Console.ReadLine()!;
        }

        return id;
    }

    public static string ValidateDate(string date)
    {
        if (date == "t")
        {
            date = DateTime.Now.ToString(Constants.Formats.Date);
        }

        while (
            !DateTime.TryParseExact(date, Constants.Formats.Date, null, DateTimeStyles.None, out _)
        )
        {
            Console.Clear();
            Console.WriteLine("The date must be in YYYY-MM-DD format.");
            Console.Write($"Please re-enter the date: ");
            date = Console.ReadLine()!;
        }

        return date;
    }

    public static string ValidateTime(string time)
    {
        if (time == "n")
        {
            time = DateTime.Now.ToString(Constants.Formats.Time);
        }

        while (
            !DateTime.TryParseExact(time, Constants.Formats.Time, null, DateTimeStyles.None, out _)
        )
        {
            Console.Clear();
            Console.WriteLine("The time must be in HH:MM (24-hour) format.");
            Console.Write($"Please re-enter the time: ");
            time = Console.ReadLine()!;
        }

        return time;
    }
}
