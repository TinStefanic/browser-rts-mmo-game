using BrowserGame.Models;

namespace BrowserGame.ModelUtils
{
	public interface IAvailableCityBuildingsManager
	{
		IEnumerable<CityBuildingType> AvailableBuildings { get; }

		bool IsAvailable(CityBuildingType cityBuildingType);
	}
}