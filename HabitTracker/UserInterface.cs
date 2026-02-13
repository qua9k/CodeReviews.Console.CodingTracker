namespace HabitTracker;

static class UserInterface
{
    public static void PrintValidatorMessage(string field)
    {
        Console.Clear();

        switch (field)
        {
            case TableFields.Id:
                Console.WriteLine("The id must be an integer greater than 0.");
                Console.Write("Please re-enter the id: ");
                break;
            case TableFields.Date:
                Console.WriteLine("The date must be in YYYY-mm-dd format.");
                Console.Write("Please re-enter the date: ");
                break;
            case TableFields.Count:
                Console.WriteLine("Habit count must be a number greater than 0.");
                Console.Write("Please re-enter the habit count: ");
                break;
            default:
                break;
        }
    }

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
}
