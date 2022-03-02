using BrowserGame.Models.Misc;

namespace BrowserGame.Models
{
	public class ResourceField : IBuilding
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int ProductionPerHour { get; set; } = 5;

		public int Level { get; set; } = 0;

		public int CropUpkeep { get; set; } = 0;

		public int? CityId { get; set; }
		public virtual City City { get; set; }

		public bool IsUpgradeInProgress { get; set; } = false;
		public BuildingType BuildingType => BuildingType.Resource;
	}
}
