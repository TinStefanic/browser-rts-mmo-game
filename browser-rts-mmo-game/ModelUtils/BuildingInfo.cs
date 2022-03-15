using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGame.Static;

namespace BrowserGame.ModelUtils
{
	public class BuildingInfo : IBuildingInfo
	{
		public string BuildingName => _cityBuildingType.ToDisplayName();
		public int ClayCost => _upgradeInfo.ClayCost;
		public int IronCost => _upgradeInfo.IronCost;
		public int WoodCost => _upgradeInfo.WoodCost;
		public int CropCost => _upgradeInfo.CropCost;

		private readonly CityBuildingType _cityBuildingType;
		private readonly UpgradeInfo _upgradeInfo;

		public BuildingInfo(CityBuildingType cityBuildingType, UpgradeInfo upgradeInfo)
		{
			_cityBuildingType = cityBuildingType;
			_upgradeInfo = upgradeInfo;
		}

		public TimeSpan GetBuildDuration(ICityManager cityManager)
		{
			return TimeSpan.FromSeconds(cityManager.GetBuildTime(_upgradeInfo));
		}

		public async Task<bool> CanBeBuiltAsync(ICityManager cityManager)
		{
			return await cityManager.CanUpgradeAsync(_upgradeInfo);
		}
	}
}
