using BrowserGame.Models;

namespace BrowserGame.Data
{
	public static class DefaultValues
	{
		public static decimal GetDefaultValue(this CityBuildingType cityBuildingType)
		{
			return cityBuildingType switch
			{
				CityBuildingType.MainBuilding => 1m,
				CityBuildingType.Wall => 1m,
				CityBuildingType.Warehouse or CityBuildingType.Granary => 1000m,
				CityBuildingType.EmptySlot => 0m, // Empty slot has no value.
				_ => throw new ArgumentException("Target CityBuildingType has no default value", nameof(cityBuildingType)),
			};
		}
	}
}
