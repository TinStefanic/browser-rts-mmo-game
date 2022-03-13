using BrowserGame.Models;

namespace BrowserGame.Internal
{
	internal interface IModelFactory
	{
		Task<Player> CreateNewPlayerAsync(string playerName, string capitalName, string userId);
	}
}
