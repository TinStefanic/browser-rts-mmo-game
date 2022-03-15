using BrowserGame.Models;
using BrowserGame.Models.Misc;
using System.Security.Claims;

namespace BrowserGame.ModelUtils
{
	public interface ICityManager
	{
		IList<CityBuilding> BuildingSlots { get; }
		int? BuildTargetLevel { get; }
		string BuildTargetName { get; }
		Clay Clay { get; }
		int ClayPerHour { get; }
		string CompletionTime { get; }
		Crop Crop { get; }
		int CropPerHour { get; }
		int Id { get; }
		Iron Iron { get; }
		int IronPerHour { get; }
		CityBuilding MainBuilding { get; }
		string Name { get; }
		CityBuilding Wall { get; }
		Wood Wood { get; }
		int WoodPerHour { get; }

		Task<bool> CanUpgradeAsync(UpgradeInfo upgradeInfo);
		int GetBuildTime(UpgradeInfo upgradeInfo);
		Task<bool> IsBuildInProgressAsync();
		bool NotUsers(ClaimsPrincipal user);
		Task<bool> TryCreateBuildingAsync(CityBuilding cityBuilding, CityBuildingType cityBuildingType);
		Task<bool> TryUpgradeAsync(IBuilding building);
	}
}