using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGame.Static;

namespace BrowserGame.Internal
{
	internal class BuildingInfoFactory
	{
		private readonly ApplicationDbContext _context;

		public BuildingInfoFactory(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<BuildingInfo> CreateNewBuildingInfoAsync(CityBuildingType cityBuildingType)
		{
			return new BuildingInfo(cityBuildingType, await _context.UpgradeInfos.FindAsync(cityBuildingType.GetUpgradeInfoId()));
		}
	}
}
