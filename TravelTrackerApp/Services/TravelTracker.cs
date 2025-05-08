using System.Text.Json;
using TravelTrackerApp.Models;

namespace TravelTrackerApp.Services;

public class TravelTracker
{
    private readonly List<VisitedLocation> _visited = [];
    private readonly List<WishlistEntry> _wishlist = [];
    private readonly string _filePath;

    public TravelTracker(string filePath = "travel_data.json")
    {
        _filePath = filePath;
        Load();
    }

    #region CRUD operations
    public void AddVisited(VisitedLocation loc) => _visited.Add(loc);
    public void AddWish(WishlistEntry wish) => _wishlist.Add(wish);

    public IReadOnlyList<VisitedLocation> Visited => _visited;
    public IReadOnlyList<WishlistEntry> Wishlist => _wishlist;

    public IEnumerable<TravelEntry> Search(string term) =>
        _visited.Cast<TravelEntry>()
                .Concat(_wishlist)
                .Where(e => e.LocationName.Contains(term, StringComparison.OrdinalIgnoreCase)
                         || e.Country.Contains(term, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<VisitedLocation> Filter(string? country = null, int? year = null) =>
        _visited.Where(v =>
            (country is null || v.Country.Equals(country, StringComparison.OrdinalIgnoreCase)) &&
            (year is null || v.DateVisited.Year == year));
    #endregion

    #region Statistics
    public int TotalPlacesVisited() => _visited.Count;
    public int CountriesExplored() => _visited.Select(v => v.Country).Distinct(StringComparer.OrdinalIgnoreCase).Count();
    public string? MostVisitedCountry() =>
        _visited.GroupBy(v => v.Country, StringComparer.OrdinalIgnoreCase)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();
    #endregion

    #region Persistence
    private record PersistModel(List<VisitedLocation> Visited, List<WishlistEntry> Wishlist);
    public void Save()
    {
        var model = new PersistModel(_visited, _wishlist);
        var json = JsonSerializer.Serialize(model, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    private void Load()
    {
        if (!File.Exists(_filePath)) return;
        var json = File.ReadAllText(_filePath);
        var model = JsonSerializer.Deserialize<PersistModel>(json);
        if (model is null) return;
        _visited.Clear(); _visited.AddRange(model.Visited);
        _wishlist.Clear(); _wishlist.AddRange(model.Wishlist);
    }
    #endregion
}
