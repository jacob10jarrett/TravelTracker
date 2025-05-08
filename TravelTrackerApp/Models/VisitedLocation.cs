using System.Globalization;
namespace TravelTrackerApp.Models;

public sealed class VisitedLocation : TravelEntry
{
    public DateTime DateVisited { get; set; }

    public override string ToDisplayString() =>
        $"{DateVisited:yyyy-MM-dd} : {LocationName}, {Country} - {Notes}";
}
