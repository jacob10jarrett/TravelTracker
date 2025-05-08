namespace TravelTrackerApp.Models;

public sealed class WishlistEntry : TravelEntry
{
    public override string ToDisplayString() =>
        $"Wishlist : {LocationName}, {Country} - {Notes}";
}
