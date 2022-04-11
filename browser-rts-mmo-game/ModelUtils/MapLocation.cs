using BrowserGame.Models;

namespace BrowserGame.ModelUtils
{
	public class MapLocation : IMapLocation
	{
		private readonly City _cityOrNull;

		public MapLocation(City cityOrNull, int x, int y)
		{
			_cityOrNull = cityOrNull;
			XCoord = x;
			YCoord = y;
		}

		public bool IsVacant => _cityOrNull == null;

		public int XCoord { get; }
		public int YCoord { get; }

		/// <inheritdoc />
		public string GetCityNameOrEmpty() => _cityOrNull?.Name ?? "";

		/// <inheritdoc />
		public MapLocationType GetMapLocationType(Player player)
		{
			if (IsVacant) return MapLocationType.Vacant;
			return player.Id == _cityOrNull.PlayerId ? MapLocationType.Owned : MapLocationType.Neutral;
		}
	}

	public enum MapLocationType
	{
		Vacant,
		Owned,
		Neutral
	}
}