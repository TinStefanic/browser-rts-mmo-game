using BrowserGame.Models;
using BrowserGame.Models.Misc;

namespace BrowserGame.ModelUtils
{
	public interface IUpgradeManager
	{
		/// <summary>
		/// Should be called when upgrade duration expires,
		/// updates building to its new level.
		/// </summary>
		/// <param name="targetId">Id of building which is being upgraded.</param>
		/// <param name="buildingType">Type of building.</param>
		/// <returns></returns>
		Task FinishUpgradeAsync(int? targetId, BuildingType buildingType);
		/// <summary>
		/// Initiates upgrade, spends resources and sets the building status to in progress.
		/// </summary>
		/// <param name="building">Building to upgrade.</param>
		/// <param name="city">City in which the building is located.</param>
		/// <returns></returns>
		Task StartUpgradeAsync(IBuilding building, City city);
	}
}