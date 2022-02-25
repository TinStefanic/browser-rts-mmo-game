using BrowserGame.Models.Misc;

namespace BrowserGame.Models
{
	public class Iron : ResourceBase
	{
		public override string Type { get; protected set; } = "Iron";

		public Iron() { }
		public Iron(int numFields)
		{
			InitFieldsList(numFields, Type);
		}
	}
}
