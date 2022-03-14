using BrowserGame.Models;
using BrowserGame.Models.Misc;
using System.Security.Claims;

namespace BrowserGame.Static
{
	public static class GameSession
	{
		public static string GetUserId(this ClaimsPrincipal user)
		{
			return user.FindFirst(ClaimTypes.NameIdentifier).Value;
		}

		public static string GetUpgradeInfoId(string buildingName, int level)
		{
			return buildingName + "#" + level.ToString();
		}
		public static string GetUpgradeInfoId(this IBuilding building)
		{
			return GetUpgradeInfoId(building.Name, building.Level);
		}

		public static string GetUpgradeInfoId(this CityBuildingType cityBuildingType)
		{
			return GetUpgradeInfoId(cityBuildingType.ToString(), 0);
		}
	}
}
