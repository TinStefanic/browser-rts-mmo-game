using BrowserGame.Models;

namespace BrowserGame.ModelUtils
{
	public interface IBuildingInfoFactory
	{
		Task<IBuildingInfo> CreateNewBuildingInfoAsync(CityBuildingType cityBuildingType);
	}
}