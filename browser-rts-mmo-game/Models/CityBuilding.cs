using BrowserGame.Models.Misc;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrowserGame.Models
{
	public class CityBuilding : IBuilding
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Level { get; set; } = 0;
		public int CropUpkeep { get; set; } = 0;
		public int? CityId { get; set; }
		public City City { get; set; }

		[Column(TypeName = "decimal(18, 2)")]
		public decimal Value { get; set; } = 0;
		public bool IsUpgradeInProgress { get; set; } = false;
		[NotMapped]
		public BuildingType BuildingType => BuildingType.CityBuilding;
		public CityBuildingType CityBuildingType { get; set; }

		public CityBuilding() { }

		public CityBuilding(City city)
		{
			City = city;
			CityBuildingType = CityBuildingType.EmptySlot;
			Name = CityBuildingType.ToString();
		}

		public CityBuilding(City city, CityBuildingType cityBuildingName, decimal value = 0m)
		{
			City = city;
			CityBuildingType = cityBuildingName;
			Name = cityBuildingName.ToString();
			Value = value;
		}
	}

	public enum CityBuildingType
	{
		MainBuilding,
		Wall,
		EmptySlot
	}
}
