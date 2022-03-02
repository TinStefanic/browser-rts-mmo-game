using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGame.Models.Misc;
using BrowserGame.Static;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BrowserGame.Internal
{
	internal class CityManager
	{
		public int Id => _city.Id;
		public string Name => _city.Name;
		public Clay Clay => _city.Clay;
		public Iron Iron => _city.Iron;
		public Wood Wood => _city.Wood;
		public Crop Crop => _city.Crop;
		/// <summary>
		///  Null if BuildQueue is empty.
		/// </summary>
		public string CompletionTime => _city.BuildQueue.CompletionTime?.ToString();
		public string BuildTargetName => _city.BuildQueue.TargetName;
		public int? BuildTargetLevel => _city.BuildQueue.TargetLevel;

		private decimal BuildingSpeed => 1.0M;

		private readonly City _city;
		private readonly ApplicationDbContext _context;

		public CityManager(City city, ApplicationDbContext context)
		{
			_city = city;
			_context = context;
		}

		public static async Task<CityManager> LoadCityManagerAsync(int cityId, ApplicationDbContext context)
		{
			City city = await context.Cities.Include(c => c.Clay.Fields)
											.Include(c => c.Wood.Fields)
											.Include(c => c.Iron.Fields)
											.Include(c => c.Crop.Fields)
											.Include(c => c.Player)
											.FirstOrDefaultAsync(m => m.Id == cityId);

			var cityManager = new CityManager(city, context);

			await cityManager.UpdateBuildQueueAsync();

			return cityManager;
		}

		public async Task<bool> TryUpgradeAsync(IBuilding building)
		{
			var upgradeManager = new UpgradeManager(_context);

			bool tryUpgradeResult = await upgradeManager.TryUpgradeAsync(building);

			if (tryUpgradeResult)
			{
				_city.BuildQueue.TargetLevel = building.Level + 1;
				_city.BuildQueue.TargetName = building.Name;
				_city.BuildQueue.CompletionTime = DateTime.Now.AddSeconds((double)upgradeManager.BuildDuration(building));
			}

			return tryUpgradeResult;
		}

		public bool NotUsers(ClaimsPrincipal user)
		{
			return _city.Player.UserId != GameSession.GetUserId(user);
		}

		public async Task<bool> IsBuildInProgressAsync()
		{
			await UpdateBuildQueueAsync();

			if (_city.BuildQueue.QueueStatus == BuildQueueStatus.Empty) return false;
			else return true;
		}

		/// <summary>
		/// In seconds.
		/// </summary>
		public int GetBuildTime(UpgradeInfo upgradeInfo)
		{
			return Convert.ToInt32(upgradeInfo.UpgradeDuration * BuildingSpeed / TimeManager.Speed);
		}

		public async Task<bool> CanUpgradeAsync(UpgradeInfo upgradeInfo)
		{
			if (upgradeInfo.ClayCost > _city.Clay.Available || upgradeInfo.IronCost > _city.Iron.Available) return false;
			if (upgradeInfo.WoodCost > _city.Wood.Available || upgradeInfo.CropCost > _city.Crop.Available) return false;
			if (upgradeInfo.IsFinnalUpgrade) return false;
			if (!_city.Crop.CanAddUpgradeBuildings) return false;
			await UpdateBuildQueueAsync();
			if (_city.BuildQueue.QueueStatus != BuildQueueStatus.Empty) return false;

			return true;
		}

		private async Task UpdateBuildQueueAsync()
		{
			if (_city.BuildQueue.QueueStatus == BuildQueueStatus.Finished)
			{
				var upgradeManager = new UpgradeManager(_context);
				await upgradeManager.FinishUpgradeAsync(_city.BuildQueue.TargetId, _city.BuildQueue.BuildingType);

				_city.BuildQueue.Clear();
			}
		}
	}
}
