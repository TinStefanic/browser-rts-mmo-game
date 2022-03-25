using BrowserGame.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrowserGame.Models
{
	public class Upgrade
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[StringLength(400)]
		public string Id
		{
			get => IdentityUtilities.GetUpgradeId(BuildingName, Level);
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

		/// <summary>
		/// In seconds.
		/// </summary>
		public int UpgradeDuration { get; set; }

		/// <summary>
		/// Represents delta change in building specific value.
		/// </summary>
		[Column(TypeName = "decimal(18, 2)")]
		public decimal ValueChangeDecimal { get; set; }

		[NotMapped]
		public int ValueChangeInt => Convert.ToInt32(ValueChangeDecimal);
	}
}
