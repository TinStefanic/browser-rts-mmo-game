using BrowserGame.Models.Misc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrowserGame.Models
{
	public class BuildQueue
	{
		public int Id { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? CompletionTime { get; set; } = null;

		public string TargetName { get; set; }

		public int? TargetId { get; set; }

		public int? TargetLevel { get; set; }

		public virtual City City { get; set; }
		public int CityId { get; set; }

		public BuildingType BuildingType { get; set; }

		[NotMapped]
		public BuildQueueStatus QueueStatus
		{
			get
			{
				if (CompletionTime == null) return BuildQueueStatus.Empty;

				if (CompletionTime >= DateTime.Now) return BuildQueueStatus.Finished;

				return BuildQueueStatus.InProgress;
			}
		}

		public BuildQueue() { }
		public BuildQueue(City city)
		{
			City = city;
		}

		public void Clear()
		{
			CompletionTime = null;
		}

		public void Add(IBuilding building, BuildingType buildingType, decimal buildDuration)
		{
			TargetId = building.Id;
			TargetName = building.Name;
			TargetLevel = building.Level;

			CompletionTime = DateTime.Now.AddSeconds((double)buildDuration);
			BuildingType = buildingType;
		}
	}

	/// <summary>
	/// Finished means that item in queue is done and should be removed.
	/// After that status is set to Empty until new item is added.
	/// </summary>
	public enum BuildQueueStatus
	{
		InProgress, Empty, Finished
	}
}
