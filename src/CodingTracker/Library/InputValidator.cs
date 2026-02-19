namespace CodingTracker.Library;

class InputValidator
{
    public static string ValidateId(string value)
    {
        if (value == "*")
        {
            return value;
        }

        while (!uint.TryParse(value, out uint _))
        {
            Console.Clear();
            Console.WriteLine("The id must be an integer greater than 0.");
            Console.Write($"Please re-enter the id: ");
            value = Console.ReadLine()!;
        }

        return value;
    }

    public static string ValidateDate(string value)
    {
        if (value == "t")
        {
            value = Convert.ToString(DateTime.Today);
            return value;
        }

        while (!DateTime.TryParse(value, out DateTime _))
        {
            Console.Clear();
            Console.WriteLine("The date must be valid and in YYYY-mm-dd format.");
            Console.Write($"Please re-enter the date: ");
            value = Console.ReadLine()!;
        }

        return value;
    }

    public static string ValidateCount(string value)
    {
        while (!uint.TryParse(value, out uint _))
        {
            Console.Clear();
            Console.WriteLine("The count must be a positive number.");
            Console.Write($"Please re-enter the count: ");
            value = Console.ReadLine()!;
        }

        return value;
    }

    public static string ValidateHabit(string value)
    {
        while (string.IsNullOrEmpty(value))
        {
            Console.Clear();
            Console.WriteLine("The habit must not be empty.");
            Console.Write($"Please re-enter the habit: ");
            value = Console.ReadLine()!;
        }

        return value;
    }
}
