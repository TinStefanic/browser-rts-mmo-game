using System.ComponentModel.DataAnnotations.Schema;

namespace BrowserGame.Models
{
	public class BuildingSlot
	{
		[NotMapped]
		public static int NumSpecialBuildingSlots => 2;
		[NotMapped]
		public static int NumSpecialBuildings => 3;
		[NotMapped]
		public static int NumBuildingSlots => 16;

		public int Id { get; set; }

		public virtual IList<CityBuilding> CityBuildings { get; set; }

		public BuildingSlot() { }
		public BuildingSlot(City city)
		{
			CityBuildings = new List<CityBuilding>
			{
				new CityBuilding(city, CityBuildingType.MainBuilding),
				new CityBuilding(city, CityBuildingType.Wall)
			};

			for (int i = 0; i < NumBuildingSlots; ++i)
			{
				CityBuildings.Add(new CityBuilding(city));
			}
		}
	}
}
