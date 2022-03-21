using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

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

		public virtual Clay Clay { get; set; }

		public virtual Wood Wood { get; set; }

		public virtual Iron Iron { get; set; }

		public virtual Crop Crop { get; set; }

		public virtual BuildingSlot BuildingSlot { get; set; }

		public virtual BuildQueue BuildQueue { get; set; }
		public City()
		{
			Clay = new Clay(4, this);
			Wood = new Wood(4, this);
			Iron = new Iron(4, this);
			Crop = new Crop(6, this);
			BuildQueue = new BuildQueue(this);
			BuildingSlot = new BuildingSlot(this);
		}
	}
}
