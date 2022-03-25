using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGame.Models.Misc;
using BrowserGame.Utilities;
using System.Security.Claims;

namespace BrowserGame.ModelUtils
{
	public static class CityUtilities
	{
		public static int GetClayPerHour(this City city) => city.Clay.ProductionPerHour * TimeManager.Speed;
		public static int GetIronPerHour(this City city) => city.Iron.ProductionPerHour * TimeManager.Speed;
		public static int GetWoodPerHour(this City city) => city.Wood.ProductionPerHour * TimeManager.Speed;
		public static int GetCropPerHour(this City city) => city.Crop.ProductionPerHour * TimeManager.Speed;
		public static IList<CityBuilding> GetBuildingSlots(this City city) =>
			city.BuildingSlot.CityBuildings.ToList().GetRange(
				BuildingSlot.NumSpecialBuildingSlots,
				BuildingSlot.NumBuildingSlots
			);
		public static CityBuilding GetMainBuilding(this City city) => 
			city.GetCityBuildingOrDefault(CityBuildingType.MainBuilding);
		public static CityBuilding GetWall(this City city) => 
			city.GetCityBuildingOrDefault(CityBuildingType.Wall);
		public static decimal GetBuildingSpeed(this City city) => city.GetMainBuilding().Value;

		/// <summary>
		/// Returns city building of given type built in the city, or null if the building isn't built.
		/// </summary>
		public static CityBuilding GetCityBuildingOrDefault(this City city, CityBuildingType type)
		{
			if (type == CityBuildingType.EmptySlot) return null;
			return city.BuildingSlot.CityBuildings.FirstOrDefault(cb => cb.CityBuildingType == type);
		}

		public static bool ContainsCityBuilding(this City city, CityBuildingType type)
		{
			return city.GetCityBuildingOrDefault(type) != null;
		}

		/// <summary>
		/// Returns value associated with the city building.
		/// If building is not built in the city, it return 
		/// default (level 0) value.
		/// </summary>
		public static decimal GetCityBuildingValue(this City city, CityBuildingType type)
		{
			CityBuilding cityBuilding = city.GetCityBuildingOrDefault(type);
			if (cityBuilding == null)
			{
				return type.GetDefaultValue();
			}
			else
			{
				return cityBuilding.Value;
			}
		}

		public static async Task UpdateBuildQueueAsync(this City city, ApplicationDbContext context)
		{
			if (city.BuildQueue.QueueStatus == BuildQueueStatus.Finished)
			{
				var upgradeManager = new UpgradeManager(context);
				await upgradeManager.FinishUpgradeAsync(city.BuildQueue.TargetId, city.BuildQueue.BuildingType);

				if (city.BuildQueue.BuildingType == BuildingType.CityBuilding)
				{
					if (city.BuildQueue.TargetName == CityBuildingType.Warehouse.ToString())
					{
						int warehouseCapacity = (int)city.GetCityBuildingValue(CityBuildingType.Warehouse);
						city.Clay.MaxCapacity = warehouseCapacity;
						city.Wood.MaxCapacity = warehouseCapacity;
						city.Iron.MaxCapacity = warehouseCapacity;
					}
					else if (city.BuildQueue.TargetName == CityBuildingType.Granary.ToString())
					{
						city.Crop.MaxCapacity = (int)city.GetCityBuildingValue(CityBuildingType.Granary);
					}
				}

				city.BuildQueue.Clear();
				await context.SaveChangesAsync();
			}
		}

		public static bool NotUsers(this City city, ClaimsPrincipal user)
		{
			return city?.Player?.UserId != user.GetUserId();
		}

		public static async Task<bool> IsBuildInProgressAsync(this City city, ApplicationDbContext context)
		{
			await city.UpdateBuildQueueAsync(context);

			if (city.BuildQueue.QueueStatus == BuildQueueStatus.Empty) return false;
			else return true;
		}

		public static int GetBuildTimeInSeconds(this City city, Upgrade upgrade)
		{
			return Convert.ToInt32(upgrade.UpgradeDuration * city.GetBuildingSpeed() / TimeManager.Speed);
		}
	}
}
