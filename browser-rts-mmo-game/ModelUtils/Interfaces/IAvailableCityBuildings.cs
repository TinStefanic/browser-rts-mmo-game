using BrowserGame.Models;

namespace BrowserGame.ModelUtils
{
	public interface IAvailableCityBuildings
	{
		IEnumerable<CityBuildingType> AvailableBuildings { get; }

		bool IsAvailable(CityBuildingType cityBuildingType);
	}
}