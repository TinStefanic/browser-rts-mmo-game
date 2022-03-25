
using BrowserGame.Models;

namespace BrowserGame.ModelUtils
{
	public interface IBuildingInfo
	{
		string BuildingName { get; }
		int ClayCost { get; }
		int CropCost { get; }
		int IronCost { get; }
		int WoodCost { get; }

		Task<bool> CanBeBuiltAsync(City city);
		TimeSpan GetBuildDuration(City city);
	}
}