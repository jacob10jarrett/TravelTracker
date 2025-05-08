using TravelTrackerApp.Models;
using TravelTrackerApp.Services;
using TravelTrackerApp.Utilities;

var tracker = new TravelTracker();
bool running = true;

void MainMenu()
{
    Console.Clear();
    Console.WriteLine("=== Travel Tracker ===");
    Console.WriteLine("1. Add visited location");
    Console.WriteLine("2. View travel history");
    Console.WriteLine("3. Add to wishlist");
    Console.WriteLine("4. View wishlist");
    Console.WriteLine("5. Search / Filter");
    Console.WriteLine("6. Statistics");
    Console.WriteLine("0. Exit");
    Console.Write("Select> ");
}

while (running)
{
    MainMenu();
    var choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            AddVisited();
            break;
        case "2":
            ListVisited();
            break;
        case "3":
            AddWishlist();
            break;
        case "4":
            ListWishlist();
            break;
        case "5":
            SearchFilter();
            break;
        case "6":
            ShowStats();
            break;
        case "0":
            tracker.Save();
            running = false;
            break;
        default:
            Console.WriteLine("Invalid selection.");
            break;
    }
}

void AddVisited()
{
    Console.Clear();
    var loc = new VisitedLocation
    {
        LocationName = ConsoleHelper.Prompt("Location name: "),
        Country = ConsoleHelper.Prompt("Country: "),
        DateVisited = ConsoleHelper.PromptDate("Date visited (YYYY‑MM‑DD): "),
        Notes = ConsoleHelper.Prompt("Notes (optional): ", allowEmpty: true)
    };
    tracker.AddVisited(loc);
    Console.WriteLine("Saved! Press any key...");
    Console.ReadKey();
}

void ListVisited()
{
    Console.Clear();
    foreach (var v in tracker.Visited.OrderBy(v => v.DateVisited))
        Console.WriteLine(v.ToDisplayString());
    Console.WriteLine("Press any key...");
    Console.ReadKey();
}

void AddWishlist()
{
    Console.Clear();
    var wish = new WishlistEntry
    {
        LocationName = ConsoleHelper.Prompt("Location name: "),
        Country = ConsoleHelper.Prompt("Country: "),
        Notes = ConsoleHelper.Prompt("Reason/Notes (optional): ", allowEmpty: true)
    };
    tracker.AddWish(wish);
    Console.WriteLine("Saved! Press any key...");
    Console.ReadKey();
}

void ListWishlist()
{
    Console.Clear();
    foreach (var w in tracker.Wishlist.OrderBy(w => w.SortKey))
        Console.WriteLine(w.ToDisplayString());
    Console.WriteLine("Press any key...");
    Console.ReadKey();
}

void SearchFilter()
{
    Console.Clear();
    Console.WriteLine("Search term (leave blank to filter):");
    var term = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(term))
    {
        foreach (var e in tracker.Search(term!))
            Console.WriteLine(e.ToDisplayString());
    }
    else
    {
        var country = ConsoleHelper.Prompt("Country filter (blank = any): ", allowEmpty: true);
        var yearStr = ConsoleHelper.Prompt("Year filter (blank = any): ", allowEmpty: true);
        int? year = int.TryParse(yearStr, out var y) ? y : null;
        foreach (var v in tracker.Filter(string.IsNullOrWhiteSpace(country) ? null : country, year))
            Console.WriteLine(v.ToDisplayString());
    }
    Console.WriteLine("Press any key...");
    Console.ReadKey();
}

void ShowStats()
{
    Console.Clear();
    Console.WriteLine($"Total Places Visited: {tracker.TotalPlacesVisited()}");
    Console.WriteLine($"Countries Explored:  {tracker.CountriesExplored()}");
    Console.WriteLine($"Most‑Visited Country: {tracker.MostVisitedCountry() ?? "N/A"}");
    Console.WriteLine("Press any key...");
    Console.ReadKey();
}
