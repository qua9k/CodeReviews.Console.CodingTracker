namespace HabitTracker;

class Validator
{
    public static string ValidateField(string fieldType, string? value)
    {
        if (fieldType == TableFields.Date)
        {
            while (!DateTime.TryParse(value, out DateTime _))
            {
                UserInterface.PrintValidatorMessage(fieldType);
                value = Console.ReadLine();
            }
        }
        else if (fieldType == TableFields.Count || fieldType == TableFields.Id)
        {
            while (!uint.TryParse(value, out uint _))
            {
                UserInterface.PrintValidatorMessage(fieldType);
                value = Console.ReadLine();
            }
        }

        return value;
    }
}
