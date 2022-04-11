using BrowserGame.Models;

namespace BrowserGame.ModelUtils
{
	public interface IModelFactory
	{
		/// <summary>
		/// Creates new Player, along with his capital city and necessary related entities.
		/// All entities are saved to the database before the Player is returned.
		/// </summary>
		/// <param name="playerName">Name of the to be created player.</param>
		/// <param name="capitalName">Name of Player's to be created capital city.</param>
		/// <param name="userId">ASP.NET Identity user Id to be linked with the to be created player.</param>
		/// <returns>Created Player.</returns>
		Task<Player> CreateNewPlayerAsync(string playerName, string capitalName, string userId);
		/// <summary>
		/// Loads city from the database, along with all related necessary entities.
		/// BuildQueue is also updated.
		/// </summary>
		/// <param name="cityId">Id of the target City.</param>
		/// <returns>City with the given id.</returns>
		Task<City> LoadCityAsync(int cityId);
	}
}
