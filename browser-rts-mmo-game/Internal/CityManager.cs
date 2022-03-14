using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGame.Models.Misc;
using BrowserGame.Static;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BrowserGame.Internal
{
	/// <summary>
	/// Class that provides interface to extract information from city and modify it.
	/// Upon modification it updates the database.
	/// </summary>
	internal class CityManager
	{
		public int Id => _city.Id;
		public string Name => _city.Name;
		public Clay Clay => _city.Clay;
		public Iron Iron => _city.Iron;
		public Wood Wood => _city.Wood;
		public Crop Crop => _city.Crop;
		public int ClayPerHour => _city.Clay.ProductionPerHour * TimeManager.Speed;
		public int IronPerHour => _city.Iron.ProductionPerHour * TimeManager.Speed;
		public int WoodPerHour => _city.Wood.ProductionPerHour * TimeManager.Speed;
		public int CropPerHour => _city.Crop.ProductionPerHour * TimeManager.Speed;
		/// <summary>
		///  Null if BuildQueue is empty.
		/// </summary>
		public string CompletionTime => _city.BuildQueue.CompletionTime?.ToString();
		public string BuildTargetName => _city.BuildQueue.TargetName;
		public int? BuildTargetLevel => _city.BuildQueue.TargetLevel;
		public IList<CityBuilding> BuildingSlots => _city.BuildingSlot.CityBuildings.ToList()
													.GetRange(BuildingSlot.NumSpecialBuildings, BuildingSlot.NumBuildingSlots);
		public CityBuilding MainBuilding => _city.BuildingSlot.CityBuildings[0];
		public CityBuilding Wall => _city.BuildingSlot.CityBuildings[1];
		private decimal BuildingSpeed => MainBuilding.Value;

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
											.Include(c => c.BuildQueue)
											.Include(c => c.BuildingSlot.CityBuildings)
											.FirstOrDefaultAsync(m => m.Id == cityId);

			var cityManager = new CityManager(city, context);

			await cityManager.UpdateBuildQueueAsync();

			return cityManager;
		}

		/// <summary>
		/// If building cannot be built returns false, otherwise true.
		/// </summary>
		public async Task<bool> TryCreateBuildingAsync(CityBuilding cityBuilding, CityBuildingType cityBuildingType)
		{
			if (new AvailableCityBuildingsManager(this).IsAvailable(cityBuildingType))
			{
				cityBuilding.CityBuildingType = cityBuildingType;

				return await TryUpgradeAsync(cityBuilding);
			}
			else
			{
				return false;
			}
		}

		public async Task<bool> TryUpgradeAsync(IBuilding building)
		{
			var upgrade = await _context.UpgradeInfos.FindAsync(building.GetUpgradeInfoId());

			if (!await CanUpgradeAsync(upgrade)) return false;

			var upgradeManager = new UpgradeManager(_context);

			await upgradeManager.StartUpgradeAsync(building, _city);

			await _context.SaveChangesAsync();

			return true;
		}

		public bool NotUsers(ClaimsPrincipal user)
		{
			return _city?.Player?.UserId != user.GetUserId();
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
				await _context.SaveChangesAsync();
			}
		}
	}
}
