using BrowserGame.Models.Misc;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrowserGame.Models
{
	public class Crop : ResourceBase
	{
		public override string Type { get; protected set; } = "Crop";

		[NotMapped]
		public int Upkeep => UpkeepBuildings;

		public int UpkeepBuildings { get; set; }

		public override int ProductionPerHour => base.ProductionPerHour - Upkeep;

		public bool CanAddUpgradeBuilding => base.ProductionPerHour - UpkeepBuildings > 10;
	}
}
