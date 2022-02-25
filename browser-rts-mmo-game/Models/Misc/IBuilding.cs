namespace BrowserGame.Models.Misc
{
	public interface IBuilding
	{
		int Id { get; set; }

		string Type { get; set; }

		int Level { get; set; }

		int CropUpkeep { get; set; }

		int? CityId { get; set; }
		City City { get; set; }
	}
}
