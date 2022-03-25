using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGame.Models.Misc;
using BrowserGame.Utilities;

namespace BrowserGame.ModelUtils
{
	/// <summary>
	/// Doesn't call save changes on context.
	/// </summary>
	public class UpgradeManager : IUpgradeManager
	{
		private readonly ApplicationDbContext _context;

		public UpgradeManager(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task StartUpgradeAsync(IBuilding building, City city)
		{
			Upgrade upgrade = await _context.Upgrades.FindAsync(building.GetUpgradeId());

			building.IsUpgradeInProgress = true;

			city.Clay.SpendResource(upgrade.ClayCost);
			city.Wood.SpendResource(upgrade.WoodCost);
			city.Iron.SpendResource(upgrade.IronCost);
			city.Crop.SpendResource(upgrade.CropCost);

			city.BuildQueue.Add(building, upgrade.UpgradeDuration / (decimal)TimeManager.Speed);
		}

		public async Task FinishUpgradeAsync(int? targetId, BuildingType buildingType)
		{
			IBuilding building;

			if (buildingType == BuildingType.Resource)
			{
				building = await _context.ResourceFields.FindAsync(targetId);
			}
			else
			{
				building = await _context.CityBuildings.FindAsync(targetId);
			}

			Upgrade upgrade = await _context.Upgrades.FindAsync(building.GetUpgradeId());

			building.Value += upgrade.ValueChangeDecimal;
			building.CropUpkeep += upgrade.AdditionalCropUpkeep;
			building.Level = upgrade.Level + 1;
			building.IsUpgradeInProgress = false;
		}
	}
}
