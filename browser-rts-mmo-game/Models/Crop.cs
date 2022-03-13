using BrowserGame.Models.Misc;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrowserGame.Models
{
	public class Crop : ResourceBase
	{
		public override string Type { get; protected set; } = typeof(Crop).ToString();

		[NotMapped]
		public int Upkeep => UpkeepBuildings;

		public int UpkeepBuildings { get; set; }

		[NotMapped]
		public override int ProductionPerHour => base.ProductionPerHour - Upkeep;

		// If crop production is to low, don't allow upgrading or creating buildings.
		[NotMapped]
		public bool CanAddUpgradeBuildings => base.ProductionPerHour - UpkeepBuildings > 10;

		public Crop() { }
		public Crop(int numFields, City city)
		{
			InitFieldsList(numFields, Type, city);
		}
	}
}
