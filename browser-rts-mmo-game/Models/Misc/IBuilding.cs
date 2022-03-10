namespace BrowserGame.Models.Misc
{
	public interface IBuilding
	{
		int Id { get; set; }

		string Name { get; set; }

		int Level { get; set; }

		int CropUpkeep { get; set; }

		int? CityId { get; set; }
		City City { get; set; }

		decimal Value { get; set; }

		bool IsUpgradeInProgress { get; set; }

		BuildingType BuildingType { get; }
	}

	public enum BuildingType
	{
		Resource, CityBuilding
	}
}
