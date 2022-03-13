using BrowserGame.Models.Misc;

namespace BrowserGame.Models
{
	public class Iron : ResourceBase
	{
		public override string Type { get; protected set; } = typeof(Iron).ToString();

		public Iron() { }
		public Iron(int numFields, City city)
		{
			InitFieldsList(numFields, Type, city);
		}
	}
}
