using CodingTracker.Library;

namespace CodingTracker.Views;

static class UserInterface
{
    public static void Pause()
    {
        Console.Write("\nPress any key to return to the main menu. ");
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

    public static string PromptForId()
    {
        Console.Clear();
        Console.Write($"Enter the id ('*' for all results): ");
        return InputValidator.ValidateId(Console.ReadLine()!);
    }

    public static string PromptForDate()
    {
        Console.Clear();
        Console.Write("Enter the date (YYYY-MM-DD or 't' for today): ");
        return InputValidator.ValidateDate(Console.ReadLine()!);
    }

    public static string PromptForStartTime()
    {
        Console.Clear();
        Console.Write("Enter the start time (HH:MM or 'n' for now): ");
        return InputValidator.ValidateTime(Console.ReadLine()!);
    }

    public static string PromptForEndTime()
    {
        Console.Clear();
        Console.Write("Enter the end time (HH:mm or 'n' for now): ");
        return InputValidator.ValidateTime(Console.ReadLine()!);
    }
}
