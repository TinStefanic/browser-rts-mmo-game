using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BrowserGame.Models
{
	[Index(nameof(Name), IsUnique = true)]
	public class City
	{
		public int Id { get; set; }

		[Required]
		public int PlayerId { get; set; }

		[Required, StringLength(50, MinimumLength = 3)]
		public string Name { get; set; }

		public int ClayId { get; set; }
		public Clay Clay { get; set; }

		public int WoodId { get; set; }
		public Wood Wood { get; set; }

		public int IronId { get; set; }
		public Iron Iron { get; set; }

		public int CropId { get; set; }
		public Crop Crop { get; set; }

	}
}
