using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BrowserGame.Models
{
	[Index(nameof(Name), IsUnique = true)]
	public class City
	{
		public int Id { get; set; }

		public int PlayerId { get; set; }
		public virtual Player Player { get; set; }

		[Display(Name = "City Name")]
		[Required, StringLength(50, MinimumLength = 3)]
		public string Name { get; set; }

		public virtual Clay Clay { get; set; } = new Clay(4);

		public virtual Wood Wood { get; set; } = new Wood(4);

		public virtual Iron Iron { get; set; } = new Iron(4);

		public virtual Crop Crop { get; set; } = new Crop(6);

		public virtual BuildQueue BuildQueue { get; set; }
		public City()
		{
			BuildQueue = new BuildQueue(this);
		}
	}
}
