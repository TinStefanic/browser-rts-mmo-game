using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGame.Models.Misc;
using BrowserGame.Static;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BrowserGame.ModelUtils
{
	/// <summary>
	/// Class that provides interface to extract information from city and modify it.
	/// Upon modification it updates the database.
	/// </summary>
	public class CityManager : ICityManager
	{
		public int Id => City.Id;
		public string Name => City.Name;
		public Clay Clay => City.Clay;
		public Iron Iron => City.Iron;
		public Wood Wood => City.Wood;
		public Crop Crop => City.Crop;
		public int ClayPerHour => City.Clay.ProductionPerHour * TimeManager.Speed;
		public int IronPerHour => City.Iron.ProductionPerHour * TimeManager.Speed;
		public int WoodPerHour => City.Wood.ProductionPerHour * TimeManager.Speed;
		public int CropPerHour => City.Crop.ProductionPerHour * TimeManager.Speed;
		/// <summary>
		///  Null if BuildQueue is empty.
		/// </summary>
		public string CompletionTime => City.BuildQueue.CompletionTime?.ToString();
		public string BuildTargetName => City.BuildQueue.TargetName;
		public int? BuildTargetLevel => City.BuildQueue.TargetLevel;
		public IList<CityBuilding> BuildingSlots => City.BuildingSlot.CityBuildings.ToList()
													.GetRange(BuildingSlot.NumSpecialBuildingSlots, BuildingSlot.NumBuildingSlots);
		public CityBuilding MainBuilding => GetCityBuilding(CityBuildingType.MainBuilding);
		public CityBuilding Wall => GetCityBuilding(CityBuildingType.Wall);
		public decimal BuildingSpeed => MainBuilding.Value;

		public City City { get; }

		private readonly ApplicationDbContext _context;

		public CityManager(City city, ApplicationDbContext context)
		{
			City = city;
			_context = context;
		}

		public static async Task<ICityManager> LoadCityManagerAsync(int cityId, ApplicationDbContext context)
		{
			var city = await new ModelFactory(context).LoadCityAsync(cityId);

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
			var upgrade = await _context.UpgradeInfos.FindAsync(building.GetUpgradeInfoId());

			if (!await CanUpgradeAsync(upgrade)) return false;

			var upgradeManager = new UpgradeManager(_context);

			await upgradeManager.StartUpgradeAsync(building, City);

			await _context.SaveChangesAsync();

			return true;
		}

		public bool NotUsers(ClaimsPrincipal user)
		{
			return City?.Player?.UserId != user.GetUserId();
		}

		public async Task<bool> IsBuildInProgressAsync()
		{
			await UpdateBuildQueueAsync();

			if (City.BuildQueue.QueueStatus == BuildQueueStatus.Empty) return false;
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
			if (upgradeInfo.ClayCost > City.Clay.Available || upgradeInfo.IronCost > City.Iron.Available) return false;
			if (upgradeInfo.WoodCost > City.Wood.Available || upgradeInfo.CropCost > City.Crop.Available) return false;
			if (upgradeInfo.IsFinnalUpgrade) return false;
			if (!City.Crop.CanAddUpgradeBuildings) return false;
			await UpdateBuildQueueAsync();
			if (City.BuildQueue.QueueStatus != BuildQueueStatus.Empty) return false;

			return true;
		}

		private async Task UpdateBuildQueueAsync()
		{
			if (City.BuildQueue.QueueStatus == BuildQueueStatus.Finished)
			{
				var upgradeManager = new UpgradeManager(_context);
				await upgradeManager.FinishUpgradeAsync(City.BuildQueue.TargetId, City.BuildQueue.BuildingType);

				if (City.BuildQueue.BuildingType == BuildingType.CityBuilding)
                {
					if (City.BuildQueue.TargetName == CityBuildingType.Warehouse.ToString())
                    {
						int warehouseCapacity = (int) GetCityBuildingValue(CityBuildingType.Warehouse);
						Clay.MaxCapacity = warehouseCapacity;
						Wood.MaxCapacity = warehouseCapacity;
						Iron.MaxCapacity = warehouseCapacity;
                    }
					else if (City.BuildQueue.TargetName == CityBuildingType.Granary.ToString())
                    {
						Crop.MaxCapacity = (int) GetCityBuildingValue(CityBuildingType.Granary);
                    }
                }

				City.BuildQueue.Clear();
				await _context.SaveChangesAsync();
			}
		}

		/// <inheritdoc/>
		public CityBuilding GetCityBuilding(CityBuildingType type)
		{
			if (type == CityBuildingType.EmptySlot) return null;
			return City.BuildingSlot.CityBuildings.FirstOrDefault(cb => cb.CityBuildingType == type);
		}

		public bool ContainsCityBuilding(CityBuildingType type)
		{
			return GetCityBuilding(type) != null;
		}

		/// <inheritdoc/>
		public decimal GetCityBuildingValue(CityBuildingType type)
		{
			CityBuilding cityBuilding = GetCityBuilding(type);
			if (cityBuilding == null)
			{
				return type.GetDefaultValue();
			}
			else
			{
				return cityBuilding.Value;
			}
		}
	}
}
