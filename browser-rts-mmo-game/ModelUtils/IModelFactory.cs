using BrowserGame.Models;

namespace BrowserGame.ModelUtils
{
	public interface IModelFactory
	{
		Task<Player> CreateNewPlayerAsync(string playerName, string capitalName, string userId);
		Task<City> LoadCityAsync(int cityId);
	}
}
