using BrowserGame.Models;

namespace BrowserGame.Internal
{
	internal class AvailableCityBuildingsManager
	{
		public IList<CityBuildingType> AvailableBuildings { get; }

		private readonly CityManager _cityManager;

		public AvailableCityBuildingsManager(CityManager cityManager)
		{
			_cityManager = cityManager;

			var availableBuildings = new HashSet<CityBuildingType>();

			for (int i = BuildingSlot.NumSpecialBuildings; i < Enum.GetValues(typeof(CityBuildingType)).Length; ++i)
			{
				availableBuildings.Add((CityBuildingType)i);
			}

			foreach (CityBuilding cityBuilding in _cityManager.BuildingSlots)
			{
				availableBuildings.Remove(cityBuilding.CityBuildingType);
			}

			AvailableBuildings = availableBuildings.ToList();
			(AvailableBuildings as List<CityBuildingType>).Sort();
		}

		/// <summary>
		/// Returns is the given building type available to be built.
		/// </summary>
		public bool IsAvailable(CityBuildingType cityBuildingType)
		{
			return AvailableBuildings.ToHashSet().Contains(cityBuildingType);
		}
	}
}
