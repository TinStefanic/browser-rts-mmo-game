using BrowserGame.Models.Misc;

namespace BrowserGame.Models
{
	public class Wood : ResourceBase
	{
		public override string Type { get; protected set; } = nameof(Wood);

		public Wood() { }
		public Wood(int numFields, City city)
		{
			InitFieldsList(numFields, Type, city);
		}
	}
}
