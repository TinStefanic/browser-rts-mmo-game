using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGame.Models.Misc;
using BrowserGame.Utilities;

namespace BrowserGame.ModelUtils
{
	public class BuildingConstructor : IBuildingConstructor
	{
		private readonly ApplicationDbContext _context;
		private readonly City _city;

		public BuildingConstructor(City city, ApplicationDbContext context)
		{
			_city = city;
			_context = context;
		}

		public async Task<bool> TryCreateBuildingAsync(CityBuilding cityBuilding, CityBuildingType cityBuildingType)
		{
			if (new AvailableCityBuildings(_city).IsAvailable(cityBuildingType))
			{
				cityBuilding.CityBuildingType = cityBuildingType;
				cityBuilding.Value = cityBuilding.CityBuildingType.GetDefaultValue();

				return await TryUpgradeAsync(cityBuilding);
			}
			else
			{
				return false;
			}
		}

		public async Task<bool> TryUpgradeAsync(IBuilding building)
		{
			var upgrade = await _context.Upgrades.FindAsync(building.GetUpgradeId());

			if (!await CanUpgradeAsync(upgrade)) return false;

			var upgradeManager = new UpgradeManager(_context);

			await upgradeManager.StartUpgradeAsync(building, _city);

			await _context.SaveChangesAsync();

			return true;
		}

		public async Task<bool> CanUpgradeAsync(Upgrade upgrade)
		{
			if (upgrade.ClayCost > _city.Clay.Available || upgrade.IronCost > _city.Iron.Available) return false;
			if (upgrade.WoodCost > _city.Wood.Available || upgrade.CropCost > _city.Crop.Available) return false;
			if (upgrade.IsFinnalUpgrade) return false;
			if (!_city.Crop.CanAddUpgradeBuildings) return false;
			await _city.UpdateBuildQueueAsync(_context);
			if (_city.BuildQueue.QueueStatus != BuildQueueStatus.Empty) return false;

			return true;
		}
	}
}
