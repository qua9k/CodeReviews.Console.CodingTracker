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

    // [[todo]] ::
    public static string ValidateDate(string date)
    {
        if (date == "t")
        {
            date = Convert.ToString(DateTime.Today);
            return date;
        }

        while (!DateTime.TryParse(date, out DateTime _))
        {
            Console.Clear();
            Console.WriteLine("The date must be valid and in YYYY-mm-dd format.");
            Console.Write($"Please re-enter the date: ");
            date = Console.ReadLine()!;
        }

        return date;
    }

    // [[todo]] ::
    public static string ValidateTime(string time)
    {
        if (time == "t")
        {
            time = Convert.ToString(DateTime.Today);
            return time;
        }

        while (!DateTime.TryParse(time, out DateTime _))
        {
            Console.Clear();
            // Console.WriteLine("The date must be valid and in YYYY-mm-dd format.");
            Console.Write($"Please re-enter the time: ");
            time = Console.ReadLine()!;
        }

        return time;
    }
}
