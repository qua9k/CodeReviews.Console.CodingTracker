namespace CodingTracker.Library;

static class UserInterface
{
    public static void Pause()
    {
        Console.WriteLine("\nPress any key to return to the main menu.");
        Console.ReadKey();
    }

    public static void PrintMenuOptions()
    {
        Console.Clear();
        Console.Write(
            """
            Please choose an option and press 'Enter':

            'c': Create entry
            'r': Read entry
            'u': Update entry
            'd': Delete entry
            'x': Exit

            Your choice: 
            """
        );
    }

    public static void PrintInputUnknown()
    {
        Console.Clear();
        Console.WriteLine("Your input was not understood.");
        Pause();
    }

    public static string PromptForHabit()
    {
        Console.Write("Enter the habit: ");
        var habit = Console.ReadLine();
        habit = Validator.ValidateField("habit", habit);
        return habit;
    }

    public static string PromptForCount()
    {
        Console.Write("Enter the count: ");
        var count = Console.ReadLine();
        count = Validator.ValidateField("count", count);
        return count;
    }

    public static string PromptForDate()
    {
        Console.Write("Enter the date (YYYY-MM-DD or 't' for today): ");

        var date = Console.ReadLine();

        if (date == "t")
        {
            date = Convert.ToString(DateTime.Today);
        }

        date = Validator.ValidateField("date", date);

        return date;
    }

    public static string PromptForId(string crudOp)
    {
        var message = $"Enter the id of the entry to {crudOp}";

        Console.Clear();

        if (crudOp == CrudOperations.Update)
        {
            Console.Write($"{message}: ");
        }
        else
        {
            Console.Write($"{message} ('*' for all results): ");
        }

        var id = Console.ReadLine();

        if (id == "*" && (crudOp == CrudOperations.Delete || crudOp == CrudOperations.Read))
        {
            return id;
        }

        return id;
    }
}
