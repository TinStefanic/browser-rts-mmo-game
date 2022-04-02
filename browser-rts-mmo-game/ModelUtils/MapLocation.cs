using BrowserGame.Models;

namespace BrowserGame.ModelUtils
{
	public class MapLocation
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

		public string GetCityNameOrEmpty() => _cityOrNull?.Name ?? "";

		public MapLocationType GetMapLocationType(Player player)
		{
			if (IsVacant) return MapLocationType.Vacant;
			else if (player.Id == _cityOrNull.PlayerId) return MapLocationType.Owned;
			else return MapLocationType.Neutral;
		}
	}

	public enum MapLocationType
	{
		Vacant,
		Owned,
		Neutral
	}
}