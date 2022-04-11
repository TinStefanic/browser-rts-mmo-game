namespace BrowserGame.ModelUtils;

public interface IMap
{
    /// <summary>
    /// Returns tuple of (XCoord, YCoord) representing randomly selected coordinates on the map,
    /// such that corresponding slot on the map is vacant.
    /// </summary>
    /// <returns>Tuple of 2 ints, representing x and y coordinate.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    Task<(int, int)> RandomFreeCoordinatesAsync();

    /// <summary>
    /// Returns MapLocation at coordinates (x mod MapWidth, y mod MapHeight).
    /// </summary>
    /// <param name="x">Target x coordinate.</param>
    /// <param name="y">Target y coordinate.</param>
    /// <returns>MapLocation at given coordinates.</returns>
    Task<MapLocation> GetMapLocationAtAsync(int x, int y);
}