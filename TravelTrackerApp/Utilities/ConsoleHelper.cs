namespace TravelTrackerApp.Utilities;

static class ConsoleHelper
{
    public static string Prompt(string message, bool allowEmpty = false)
    {
        Console.Write(message);
        var input = Console.ReadLine() ?? string.Empty;
        return allowEmpty ? input : input.Trim();
    }

    public static DateTime PromptDate(string message)
    {
        while (true)
        {
            var input = Prompt(message);
            if (DateTime.TryParse(input, out var date)) return date;
            Console.WriteLine("Invalid date ‑ try again (e.g., 2025‑05‑15).");
        }
    }
}
