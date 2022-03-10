using BrowserGame.Models.Misc;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrowserGame.Models
{
	public class ResourceField : IBuilding
	{
		public int Id { get; set; }

		public string Name { get; set; }

		[NotMapped]
		public int ProductionPerHour => Convert.ToInt32(Value);

		[Column(TypeName = "decimal(18, 2)")]
		public decimal Value { get; set; } = 5;

		public int Level { get; set; } = 0;

		public int CropUpkeep { get; set; } = 0;

		public int? CityId { get; set; }
		public virtual City City { get; set; }

		public bool IsUpgradeInProgress { get; set; } = false;
		[NotMapped]
		public BuildingType BuildingType => BuildingType.Resource;
	}
}
