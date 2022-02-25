using BrowserGame.Models.Misc;

namespace BrowserGame.Models
{
	public class Wood : ResourceBase
	{
		public override string Type { get; protected set; } = "Wood";

		public Wood() { }
		public Wood(int numFields)
		{
			InitFieldsList(numFields, Type);
		}
	}
}
