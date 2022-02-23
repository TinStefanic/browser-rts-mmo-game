using BrowserGame.Models.Misc;

namespace BrowserGame.Models
{
	public class ResourceField : IBuilding
	{
		public int Id { get; set; }

		public string Type { get; set; }

		public int ProductionPerHour { get; set; } = 5;

		public int Level { get; set; } = 0;

		public int CropUpkeep { get; set; } = 0;

		public int UpgradeInfoId { get; set; }
		public UpgradeInfo UpgradeInfo { get; set; }
	}
}
