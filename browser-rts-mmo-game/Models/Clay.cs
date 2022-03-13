using BrowserGame.Models.Misc;

namespace BrowserGame.Models
{
	public class Clay : ResourceBase
	{
		public override string Type { get; protected set; } = typeof(Clay).Name;

		public Clay() { }
		public Clay(int numFields, City city)
		{
			InitFieldsList(numFields, Type, city);
		}
	}
}
