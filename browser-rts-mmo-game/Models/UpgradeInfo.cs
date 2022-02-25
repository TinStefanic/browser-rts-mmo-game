using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrowserGame.Models
{
	public class UpgradeInfo
	{
		// Key is in format:
		// BuildingName#Level
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[StringLength(400)]
		public string Id
		{
			get => BuildingName + "#" + Level.ToString();
			private set { ; }
		}

		public string BuildingName { get; set; }

		public int ClayCost { get; set; }
		
		public int WoodCost { get; set; }

		public int IronCost { get; set; }

		public int CropCost { get; set; }

		public bool IsFinnalUpgrade { get; set; }

		public int Level { get; set; }

		public int AdditionalCropUpkeep { get; set; }

		// Represents delta change in building specific value.
		[Column(TypeName = "decimal(18, 2)")]
		public decimal ValueChange { get; set; }

		[NotMapped]
		public int ValueChangeInt => Convert.ToInt32(ValueChange);
	}
}
