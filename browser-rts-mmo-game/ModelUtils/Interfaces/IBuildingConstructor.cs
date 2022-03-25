using BrowserGame.Models;
using BrowserGame.Models.Misc;

namespace BrowserGame.ModelUtils
{
	public interface IBuildingConstructor
	{
		Task<bool> CanUpgradeAsync(Upgrade upgrade);
		Task<bool> TryCreateBuildingAsync(CityBuilding cityBuilding, CityBuildingType cityBuildingType);
		Task<bool> TryUpgradeAsync(IBuilding building);
	}
}