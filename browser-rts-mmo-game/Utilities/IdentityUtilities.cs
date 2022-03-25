using BrowserGame.Models;
using BrowserGame.Models.Misc;
using System.Security.Claims;

namespace BrowserGame.Utilities
{
	public static class IdentityUtilities
	{
		public static string GetUserId(this ClaimsPrincipal user)
		{
			return user.FindFirst(ClaimTypes.NameIdentifier).Value;
		}

		public static string GetUpgradeId(string buildingName, int level)
		{
			return buildingName + "#" + level.ToString();
		}
		public static string GetUpgradeId(this IBuilding building)
		{
			return GetUpgradeId(building.Name, building.Level);
		}

		public static string GetUpgradeId(this CityBuildingType cityBuildingType)              
		{
			return GetUpgradeId(cityBuildingType.ToString(), 0);
		}
	}
}
