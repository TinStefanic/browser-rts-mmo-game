using BrowserGame.Models;

namespace BrowserGame.ModelUtils;

public interface IMapLocation
{
    bool IsVacant { get; }
    int XCoord { get; }
    int YCoord { get; }
    /// <summary>
    /// Returns Name of city at this location, or string.Empty if there is no city.
    /// </summary>
    /// <returns>Name of city, or empty string.</returns>
    string GetCityNameOrEmpty();
    /// <summary>
    /// Returns MapLocationType corresponding to this location,
    /// from the perspective of the player.
    /// </summary>
    /// <param name="player">Player from whose perspective MapLocationType is determined.</param>
    /// <returns>MapLocationType enum.</returns>
    MapLocationType GetMapLocationType(Player player);
}