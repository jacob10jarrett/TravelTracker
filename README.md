# TravelTracker Console Application

## Author
Jacob Jarrett — ITCS 3112 final project.

A clean, object‑oriented C# 12 (.NET 8) console application for recording trips and planning future travel.
## Features
* Add visited locations with date, country, and notes
* Maintain a wishlist of destinations
* View, search, filter, and sort records
* Quick statistics (total places, countries, most‑visited country)
* JSON persistence across sessions

## Folder structure
```
TravelTrackerApp/
  Models/
  Services/
  Utilities/
  Program.cs
```

## Build & Run

1. Install [.NET 8 SDK](https://dotnet.microsoft.com/download).
2. Open a terminal in **TravelTrackerApp** and run:
   ```bash
   dotnet run
   ```
3. Data is saved to `travel_data.json` in the executable folder.
