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

            'c': Create new coding session
            'r': Read past coding session
            'u': Update past coding session
            'd': Delete past coding session
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
        Console.Clear();
        Console.Write("Enter the habit: ");
        var habit = Console.ReadLine();
        return Validator.ValidateHabit(habit!);
    }

    public static string PromptForCount()
    {
        Console.Clear();
        Console.Write("Enter the count: ");
        var count = Console.ReadLine();
        return Validator.ValidateCount(count!);
    }

    public static string PromptForDate()
    {
        Console.Clear();
        Console.Write("Enter the date (YYYY-MM-DD or 't' for today): ");
        var date = Console.ReadLine();
        return Validator.ValidateDate(date!);
    }

    public static string PromptForId()
    {
        Console.Clear();
        Console.Write($"Enter the entry id ('*' for all results): ");
        var id = Console.ReadLine();
        return Validator.ValidateId(id!);
    }
}
