using BrowserGame.Static;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrowserGame.Models.Misc
{
	public abstract class ResourceBase
	{
		public int Id { get; set; }

		public IList<ResourceField> Fields { get; set; } = new List<ResourceField>();

		public int MaxCapacity { get; set; } = 1000;

		[NotMapped]
		/// <summary>
		/// Returns how much of this resource is produced each hour.
		/// </summary>
		public virtual int ProductionPerHour => Fields.Select(rf => rf.ProductionPerHour).ToList().Sum();

		/// <summary>
		/// Can be "Clay", "Wood", "Iron" or "Crop".
		/// </summary>
		public abstract string Type { get; protected set; }

		/// <summary>
		/// Time of last call of GetValue method, represented with DateTime.Now.Ticks.
		/// </summary>
		public long LastUpdate { get; protected set; }

		protected decimal _available;
		/// <summary>
		/// Returns how much of this resource is currently available.
		/// </summary>
		public decimal Available
		{
			get => GetAvailable();
			protected set => _available = value;
		}

		private decimal GetAvailable()
		{
			long currentTime = TimeManager.Ticks;
			decimal elapsedHours = (decimal)new TimeSpan(currentTime - LastUpdate).TotalHours;
			LastUpdate = currentTime;

			_available = Math.Min(elapsedHours * ProductionPerHour, MaxCapacity);
			return _available;
		}
	}
}
