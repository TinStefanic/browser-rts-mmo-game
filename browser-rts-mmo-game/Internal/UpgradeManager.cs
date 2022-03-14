using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGame.Models.Misc;
using BrowserGame.Static;

namespace BrowserGame.Internal
{
	/// <summary>
	/// Doesn't call save changes on context.
	/// </summary>
	internal class UpgradeManager
	{
		private readonly ApplicationDbContext _context;

		public UpgradeManager(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task StartUpgradeAsync(IBuilding building, City city)
		{
			UpgradeInfo upgrade = await _context.UpgradeInfos.FindAsync(building.GetUpgradeInfoId());
			
			building.IsUpgradeInProgress = true;

			city.Clay.SpendResource(upgrade.ClayCost);
			city.Wood.SpendResource(upgrade.WoodCost);
			city.Iron.SpendResource(upgrade.IronCost);
			city.Crop.SpendResource(upgrade.CropCost);

			city.BuildQueue.Add(building, upgrade.UpgradeDuration / (decimal) TimeManager.Speed);
		}

		public async Task FinishUpgradeAsync(int? targetId, BuildingType buildingType)
		{
			if (buildingType == BuildingType.Resource)
			{
				ResourceField resource = await _context.ResourceFields.FindAsync(targetId);
				UpgradeInfo upgrade = await _context.UpgradeInfos.FindAsync(resource.GetUpgradeInfoId());

				resource.Value += upgrade.ValueChangeInt;

				FinishUpgrade(resource, upgrade);
			}
		}

		private static void FinishUpgrade(IBuilding building, UpgradeInfo upgrade)
		{
			building.CropUpkeep += upgrade.AdditionalCropUpkeep;
			building.Level = upgrade.Level + 1;
			building.IsUpgradeInProgress = false;
		}
	}
}
