using BrowserGame.Models.Misc;

namespace BrowserGame.Models
{
	public class Clay : ResourceBase
	{
		public override string Type { get; protected set; } = "Clay";

		public Clay() { }
		public Clay(int numFields)
		{
			InitFieldsList(numFields, Type);
		}
	}
}
