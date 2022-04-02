using BrowserGame.ModelUtils;

namespace BrowserGame.Utilities
{
	public interface IHtmlColors
	{
		string BuildingSlotBuilding { get; }
		string Clay { get; }
		string Crop { get; }
		string EmptySlot { get; }
		string Iron { get; }
		string MainBuilding { get; }
		string Wall { get; }
		string Wood { get; }

		string GetColor(MapLocationType mapLocationType);
	}
}