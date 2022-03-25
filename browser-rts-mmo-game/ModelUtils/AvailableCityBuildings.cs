using BrowserGame.Models;

namespace BrowserGame.ModelUtils
{
	public class AvailableCityBuildings : IAvailableCityBuildings
	{
		public IEnumerable<CityBuildingType> AvailableBuildings { get; }

		private readonly City _city;

		public AvailableCityBuildings(City city)
		{
			_city = city;

			var availableBuildings = new SortedSet<CityBuildingType>();

			for (int i = BuildingSlot.NumSpecialBuildings; i < Enum.GetValues(typeof(CityBuildingType)).Length; ++i)
			{
				availableBuildings.Add((CityBuildingType)i);
			}

			foreach (CityBuilding cityBuilding in _city.GetBuildingSlots())
			{
				availableBuildings.Remove(cityBuilding.CityBuildingType);
			}

			AvailableBuildings = availableBuildings.ToList();
		}

		/// <summary>
		/// Returns is the given building type available to be built.
		/// </summary>
		public bool IsAvailable(CityBuildingType cityBuildingType)
		{
			return AvailableBuildings.Contains(cityBuildingType);
		}
	}
}
