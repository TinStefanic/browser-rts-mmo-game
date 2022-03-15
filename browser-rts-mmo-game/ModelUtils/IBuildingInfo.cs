
namespace BrowserGame.ModelUtils
{
	public interface IBuildingInfo
	{
		string BuildingName { get; }
		int ClayCost { get; }
		int CropCost { get; }
		int IronCost { get; }
		int WoodCost { get; }

		Task<bool> CanBeBuiltAsync(ICityManager cityManager);
		TimeSpan GetBuildDuration(ICityManager cityManager);
	}
}