using BrowserGame.Models;
using BrowserGame.Models.Misc;

namespace BrowserGame.ModelUtils
{
	public interface IUpgradeManager
	{
		Task FinishUpgradeAsync(int? targetId, BuildingType buildingType);
		Task StartUpgradeAsync(IBuilding building, City city);
	}
}