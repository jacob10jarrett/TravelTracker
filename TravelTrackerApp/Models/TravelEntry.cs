namespace TravelTrackerApp.Models;

public abstract class TravelEntry
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string LocationName { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;

    /// <summary>Used for quick alphabetical ordering.</summary>
    public string SortKey => $"{Country}|{LocationName}".ToLowerInvariant();

    public abstract string ToDisplayString();
}
