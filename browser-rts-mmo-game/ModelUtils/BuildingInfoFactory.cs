using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGame.Utilities;

namespace BrowserGame.ModelUtils
{
	public class BuildingInfoFactory : IBuildingInfoFactory
	{
		private readonly ApplicationDbContext _context;

		public BuildingInfoFactory(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IBuildingInfo> CreateNewBuildingInfoAsync(CityBuildingType cityBuildingType)
		{
			return new BuildingInfo(
				cityBuildingType, 
				await _context.Upgrades.FindAsync(cityBuildingType.GetUpgradeId()),
				_context
			);
		}
	}
}
