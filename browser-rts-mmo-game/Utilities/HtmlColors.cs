using BrowserGame.ModelUtils;

namespace BrowserGame.Utilities
{
	public class HtmlColors : IHtmlColors
	{
		public string Clay => "danger";
		public string Iron => "secondary";
		public string Wood => "success";
		public string Crop => "warning";
		public string MainBuilding => "primary";
		public string BuildingSlotBuilding => "warning";
		public string Wall => "secondary";
		public string EmptySlot => "success";

		public string GetColor(MapLocationType mapLocationType)
		{
			throw new NotImplementedException();
		}
	}
}
