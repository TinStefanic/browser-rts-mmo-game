using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGame.Utilities;

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
		private readonly Upgrade _upgradeInfo;
		private readonly ApplicationDbContext _context;

		public BuildingInfo(CityBuildingType cityBuildingType, Upgrade upgrade, ApplicationDbContext context)
		{
			_cityBuildingType = cityBuildingType;
			_upgradeInfo = upgrade;
			_context = context;
		}

		public TimeSpan GetBuildDuration(City city)
		{
			return TimeSpan.FromSeconds(city.GetBuildTimeInSeconds(_upgradeInfo));
		}

		public async Task<bool> CanBeBuiltAsync(City city)
		{
			var buildingConstructor = new BuildingConstructor(city, _context);
			return await buildingConstructor.CanUpgradeAsync(_upgradeInfo);
		}
	}
}
