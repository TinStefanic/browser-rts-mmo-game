using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGame.Models.Misc;
using BrowserGame.Static;

namespace BrowserGame.Internal
{
	internal class UpgradeManager
	{
		private readonly ApplicationDbContext _context;

		public UpgradeManager(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<bool> TryUpgradeAsync(IBuilding building)
		{
			throw new NotImplementedException();
		}

		public async Task FinishUpgradeAsync(int? targetId, BuildingType buildingType)
		{
			if (buildingType == BuildingType.Resource)
			{
				ResourceField resource = await _context.ResourceFields.FindAsync(targetId);
				UpgradeInfo upgrade = await _context.UpgradeInfos.FindAsync(GameSession.GetUpgradeInfoId(resource));

				resource.ProductionPerHour = upgrade.ValueChangeInt;
				FinishUpgradeAsync(resource, upgrade);
			}
		}

		private void FinishUpgradeAsync(IBuilding building, UpgradeInfo upgrade)
		{
			building.CropUpkeep += upgrade.AdditionalCropUpkeep;
			building.Level = upgrade.Level + 1;
			building.IsUpgradeInProgress = false;
		}

		internal double BuildDuration(IBuilding building)
		{
			throw new NotImplementedException();
		}
	}
}
